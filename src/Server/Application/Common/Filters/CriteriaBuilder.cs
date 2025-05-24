// Copyright (c) 2025 - Jun Dev. All rights reserved

using System.Linq.Expressions;

namespace Application.Common.Filters;

public sealed class CriteriaBuilder<TModel> : ICriteriaBuilder<TModel>
{
	/// <summary>Dictionary to store allowed operators for different types</summary>
	private static Dictionary<Type[], string[]> AllowOperators = new()
	{
		{
			new [] { typeof(string) },
			new [] { "eq", "ne", "c", "sw", "ew" }
		},
		{
			new [] { typeof(int), typeof(decimal), typeof(double), typeof(long), typeof(float), typeof(DateTime) },
			new [] { "eq", "ne", "gt", "gte", "lt", "lte" }
		},
		{
			new [] { typeof(bool) },
			new [] { "eq", "ne" }
		}
	};
	private static string[] AllowSortValue = new[] { "asc", "desc" };

	public IQueryable<TModel> Apply(
		IQueryable<TModel> query,
		IEnumerable<QueryFilter>? filters = null,
		IEnumerable<QuerySort>? sorts = null)
	{
		// Apply filters to query
		if ((filters != null) && filters.Any())
		{
			foreach (var filter in filters)
			{
				if (filter.Field != "fullText")
					query = ApplyFilter(query, filter);
				else
					query = ApplySearchFullText(query, filter.Value);
			}
		}

		// Apply sorting to query
		if ((sorts != null) && sorts.Any())
		{
			var isFirstSort = true;
			foreach (var sort in sorts)
			{
				query = ApplySort(query, sort, isFirstSort);
				isFirstSort = false;
			}
		}

		return query;
	}

	private IQueryable<TModel> ApplyFilter(
		IQueryable<TModel> query,
		QueryFilter filter)
	{
		if (string.IsNullOrWhiteSpace(filter.Value)) return query;

		var parameter = Expression.Parameter(typeof(TModel), "x");
		if (!ReflectionHelper.TryGetProperty(typeof(TModel), filter.Field, out _)) return query;

		var property = Expression.Property(parameter, filter.Field);
		var propertyType = property.Type;

		// Validate operator compatibility
		if (!IsValidOperator(filter, propertyType))
			throw new NotSupportedException("One or more filters are invalid or contain incorrect data types.");

		// Try parse value to match property type
		var targetType = Nullable.GetUnderlyingType(propertyType) ?? propertyType;
		if (!TryParseValue(filter.Value, targetType, out var value))
			throw new NotSupportedException("One or more filters are invalid or contain incorrect data types.");

		// Important fix: constant must match property.Type (including Nullable<T>)
		var constant = Expression.Constant(value, propertyType);

		Expression body = filter.Operator.ToLower() switch
		{
			"eq" => Expression.Equal(property, constant),
			"ne" => Expression.NotEqual(property, constant),
			"gt" => Expression.GreaterThan(property, constant),
			"gte" => Expression.GreaterThanOrEqual(property, constant),
			"lt" => Expression.LessThan(property, constant),
			"lte" => Expression.LessThanOrEqual(property, constant),
			"c" => Expression.Call(property, "Contains", null, constant),
			"sw" => Expression.Call(property, "StartsWith", null, constant),
			"ew" => Expression.Call(property, "EndsWith", null, constant),
			_ => throw new NotSupportedException($"Operator '{filter.Operator}' is not supported.")
		};

		var lambda = Expression.Lambda<Func<TModel, bool>>(body, parameter);
		return query.Where(lambda);
	}

	private IQueryable<TModel> ApplySearchFullText(
		IQueryable<TModel> query,
		string searchText)
	{
		//return query.Where(prop => EF.Functions.Matches(
		//	EF.Property<NpgsqlTsVector>(prop, "SearchVector"),
		//	EF.Functions.PlainToTsQuery("simple", searchText)
		//));
		return query;
	}

	private IQueryable<TModel> ApplySort(
		IQueryable<TModel> query,
		QuerySort sort,
		bool isFirstSort = false)
	{
		if (!AllowSortValue.Contains(sort.Direction))
			throw new NotSupportedException("One or more sorts are invalid data values.");

		var parameter = Expression.Parameter(typeof(TModel), "x");
		var isExistProperty = ReflectionHelper.TryGetProperty(typeof(TModel), sort.Field, out _);
		if (!isExistProperty) return query;

		var property = Expression.Property(parameter, sort.Field);
		var lambda = Expression.Lambda(property, parameter);

		// Get sort method name
		var isDescendingSort = sort.Direction.ToLower() == "desc";
		var methodName = isFirstSort
			? (isDescendingSort ? "OrderByDescending" : "OrderBy")
			: (isDescendingSort ? "ThenByDescending" : "ThenBy");

		var resultExpression = Expression.Call(
			typeof(Queryable),
			methodName,
			new Type[] { typeof(TModel), property.Type },
			query.Expression,
			Expression.Quote(lambda));
		return query.Provider.CreateQuery<TModel>(resultExpression);
	}

	private bool IsValidOperator(QueryFilter filter, Type rawType)
	{
		var type = Nullable.GetUnderlyingType(rawType) ?? rawType;

		foreach (var entry in AllowOperators)
		{
			if (entry.Key.Contains(type))
				return entry.Value.Contains(filter.Operator.ToLower());
		}
		return false;
	}

	private bool TryParseValue(string value, Type rawType, out object? result)
	{
		result = null;
		if (string.IsNullOrWhiteSpace(value))
			return false;

		var type = Nullable.GetUnderlyingType(rawType) ?? rawType;

		if (type == typeof(DateTime))
		{
			if (DateTime.TryParse(value, out var dt))
			{
				result = DateTime.SpecifyKind(dt, DateTimeKind.Utc);
				return true;
			}
			return false;
		}

		if (type == typeof(bool))
		{
			if (bool.TryParse(value, out var b))
			{
				result = b;
				return true;
			}
			return false;
		}

		if (type == typeof(Guid))
		{
			if (Guid.TryParse(value, out var g))
			{
				result = g;
				return true;
			}
			return false;
		}

		if (type.IsEnum)
		{
			try
			{
				result = Enum.Parse(type, value, ignoreCase: true);
				return true;
			}
			catch
			{
				return false;
			}
		}

		try
		{
			result = Convert.ChangeType(value, type);
			return true;
		}
		catch
		{
			return false;
		}
	}
}
