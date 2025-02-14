using CleanArchitecture.Application.Abstractions;
using CleanArchitecture.Application.Features.AuthFeatures.Commands.Login;
using CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CleanArchitecture.Infrastructure.Authentication;

public sealed class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _jwtOptions;
    private readonly UserManager<AppUser> _userManager;

    public JwtProvider(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }

    public async Task<LoginCommandResponse> CreateTokenAsync(AppUser appUser)
    {
        var claims = new Claim[]
        {
            new Claim(ClaimTypes.Email,appUser.Email),
            new Claim(JwtRegisteredClaimNames.Name,appUser.UserName),
            new Claim("NameLastName",appUser.NameLastName)
        };

        DateTime expires = DateTime.Now.AddHours(1);

        JwtSecurityToken jwtSecurityToken = new(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: null,
            notBefore: DateTime.Now,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),SecurityAlgorithms.HmacSha256));

        string token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        string refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
        appUser.RefreshToken = refreshToken;
        appUser.RefreshTokenExpiryTime = expires.AddMinutes(15);
        await _userManager.UpdateAsync(appUser);

        LoginCommandResponse response = new(token,refreshToken,appUser.RefreshTokenExpiryTime,appUser.Id,appUser.UserName,appUser.Email,appUser.NameLastName);

        return response;

    }
}
