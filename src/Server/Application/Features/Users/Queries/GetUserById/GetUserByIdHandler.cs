// Copyright (c) 2025 - Jun Dev. All rights reserved

using Application.Features.Users.Dtos;

namespace Application.Features.Users.Queries.GetTaskById;

public sealed class GetUserByIdHandler : IQueryHandler<GetUserByIdQuery, UserDto>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetUserByIdHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
	{
		var targetUser = await _unitOfWork.Users.GetUserByIdAsync(request.UserId, cancellationToken);
		if (targetUser is null) throw new NotFoundException("User", request.UserId);

		return targetUser.Adapt<UserDto>();
	}
}
