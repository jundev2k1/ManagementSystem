// Copyright (c) 2025 - Jun Dev. All rights reserved

namespace Application.Common.Interfaces.Repositories;

public interface IUserRepository
{
	Task<User?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default);

	Task<User?> GetUserByUsernameAsync(string username, CancellationToken cancellationToken = default);

	Task<Guid> CreateNewUserAsync(User user, CancellationToken cancellationToken = default);

	Task UpdateTaskAsync(User user, CancellationToken cancellationToken = default);

	Task DeleteTaskAsync(Guid id, CancellationToken cancellationToken = default);
}
