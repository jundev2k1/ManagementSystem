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
			.FirstOrDefaultAsync(u => u.UserId == id, cancellationToken);
		return targetTask;
	}

	public async Task<User?> GetUserByUserNameAsync(string userName, CancellationToken cancellationToken = default)
	{
		var targetTask = await dbContext.Users
			.AsNoTracking()
			.FirstOrDefaultAsync(u => u.UserName == userName, cancellationToken);
		return targetTask;
	}

	public async Task<bool> IsExistsUserName(string userName, CancellationToken cancellationToken = default)
	{
		return await dbContext.Users
			.AsNoTracking()
			.AnyAsync(u => u.UserName == userName, cancellationToken);
	}

	public async Task<bool> IsExistsEmail(string email, CancellationToken cancellationToken = default)
	{
		return await dbContext.Users
			.AsNoTracking()
			.AnyAsync(u => u.Email == email, cancellationToken);
	}

	public async Task<Guid> CreateNewUserAsync(User user, CancellationToken cancellationToken = default)
	{
		await dbContext.Users.AddAsync(user, cancellationToken);
		return user.UserId;
	}

	public async Task UpdateTaskAsync(User user, CancellationToken cancellationToken = default)
	{
		var targetUser = await dbContext.Users
			.FirstOrDefaultAsync(u => u.UserId == user.UserId, cancellationToken);
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
			.FirstOrDefaultAsync(u => u.UserId == id, cancellationToken);
		if (targetUser is null) throw new NotFoundException(name: "User", key: id);

		dbContext.Users.Remove(targetUser);
	}
}
