// Copyright (c) 2025 - Jun Dev. All rights reserved

using Application.Common.Interfaces.Repositories;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public sealed class UserRepository(ApplicationDbContext dbContext) : IUserRepository
{
	public async Task<User?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default)
	{
		var targetTask = await dbContext.Users
			.AsNoTracking()
			.FirstOrDefaultAsync(t => t.UserId == id, cancellationToken);
		return targetTask;
	}

	public async Task<User?> GetUserByUserNameAsync(string userName, CancellationToken cancellationToken = default)
	{
		var targetTask = await dbContext.Users
			.AsNoTracking()
			.FirstOrDefaultAsync(t => t.UserName == userName, cancellationToken);
		return targetTask;
	}

	public async Task<Guid> CreateNewUserAsync(User user, CancellationToken cancellationToken = default)
	{
		await dbContext.Users.AddAsync(user, cancellationToken);
		return user.UserId;
	}

	public async Task UpdateTaskAsync(User user, CancellationToken cancellationToken = default)
	{
		var targetUser = await dbContext.Users
			.FirstOrDefaultAsync(t => t.UserId == user.UserId, cancellationToken);
		if (targetUser is null) throw new NotFoundException(name: "User", key: user.UserId);

		// Set the properties of the target task to the values from the source task
		targetUser.Email = user.Email;
		targetUser.PasswordHash = user.PasswordHash;
		targetUser.Avatar = user.Avatar;
		targetUser.FirstName = user.FirstName;
		targetUser.LastName = user.LastName;
		targetUser.PhoneNumber = user.PhoneNumber;
		targetUser.Address = user.Address;
		targetUser.ValidFlg = user.ValidFlg;
		targetUser.LastModifiedAt = DateTime.UtcNow;
		targetUser.LastModifiedBy = user.LastModifiedBy;
	}

	public async Task DeleteTaskAsync(Guid id, CancellationToken cancellationToken = default)
	{
		var targetUser = await dbContext.Users.AsNoTracking()
			.FirstOrDefaultAsync(t => t.UserId == id, cancellationToken);
		if (targetUser is null) throw new NotFoundException(name: "User", key: id);

		dbContext.Users.Remove(targetUser);
	}
}
