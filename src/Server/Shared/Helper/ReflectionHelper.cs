// Copyright(c) 2025 - Jun Dev.All rights reserved

using System.Reflection;

namespace Shared.Helper;

public static class ReflectionHelper
{
	public static bool TryGetProperty(Type type, string propertyName, out PropertyInfo? propertyInfo)
	{
		propertyInfo = type.GetProperty(propertyName,
			BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
		return propertyInfo != null;
	}
}
