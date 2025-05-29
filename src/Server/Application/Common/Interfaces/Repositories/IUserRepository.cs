// Copyright (c) 2025 - Jun Dev. All rights reserved

using Application.Common.Abstractions.Pagination;

namespace Application.Common.Interfaces.Repositories;

public interface IUserRepository
{
	Task<PaginationResult<User>> GetTasksByCriteriaAsync(
		Func<IQueryable<User>, IQueryable<User>>? queryBuilder = null,
		int page = 1,
		int pageSize = 20,
		CancellationToken cancellationToken = default);

	Task<User?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default);

	Task<User?> GetUserByUserNameAsync(string userName, CancellationToken cancellationToken = default);

	Task<bool> IsExistsUserName(string email, CancellationToken cancellationToken = default);

	Task<bool> IsExistsEmail(string email, CancellationToken cancellationToken = default);

	Task<Guid> CreateNewUserAsync(User user, CancellationToken cancellationToken = default);

	Task UpdateUserAsync(User user, CancellationToken cancellationToken = default);

	Task DeleteUserAsync(Guid id, CancellationToken cancellationToken = default);
}
