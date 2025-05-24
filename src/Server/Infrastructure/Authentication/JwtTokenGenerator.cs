// Copyright(c) 2025 - Jun Dev.All rights reserved

using Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Authentication;

public sealed class JwtTokenGenerator : IJwtTokenGenerator
{
	private readonly IConfiguration _configuration;
	private readonly JwtSettings _jwtSettings;
	public JwtTokenGenerator(IConfiguration configuration)
	{
		_configuration = configuration;
		_jwtSettings = _configuration.GetSection("JwtSettings")
			.Get<JwtSettings>() ?? new JwtSettings();
	}

	public string GenerateJwtToken(Guid userId, string email, string[] roles)
	{
		var key = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey!);
		var claims = new List<Claim>
		{
			new Claim(JwtRegisteredClaimNames.Jti, userId.ToString()),
			new Claim(JwtRegisteredClaimNames.Email, email),
		};
		claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

		var tokenDescriptor = new SecurityTokenDescriptor
		{
			Issuer = _jwtSettings.Issuer,
			Audience = _jwtSettings.Audience,
			Subject = new ClaimsIdentity(claims),
			Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
			SigningCredentials = new SigningCredentials(
				new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
		};
		var tokenHandler = new JwtSecurityTokenHandler();
		var token = tokenHandler.CreateToken(tokenDescriptor);
		return tokenHandler.WriteToken(token);
	}
}
