using AutoMapper;
using CleanArchitecture.Application.Abstractions;
using CleanArchitecture.Application.Features.AuthFeatures.Commands.Login;
using CleanArchitecture.Application.Features.AuthFeatures.Commands.Register;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Persistance.Services;

public sealed class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;
    private readonly IJwtProvider _jwtProvider;
    public AuthService(UserManager<AppUser> userManager, IMapper mapper, IJwtProvider jwtProvider)
    {
        _userManager = userManager;
        _mapper = mapper;
        _jwtProvider = jwtProvider;
    }
    public async Task<LoginCommandResponse> LoginAsync(LoginCommand request, CancellationToken cancellationToken)
    {
        AppUser? user =
            await _userManager.Users
            .Where(
                p => p.UserName == request.UserNameOrEmail
                  || p.Email == request.UserNameOrEmail)
            .FirstOrDefaultAsync(cancellationToken);
        if (user == null) throw new Exception("Kullanıcı bulunamadı!");
        var result = await _userManager.CheckPasswordAsync(user, request.Password);
        if (result)
        {
            LoginCommandResponse response = await _jwtProvider.CreateTokenAsync(user);
            return response;
        }
        throw new Exception("Şifreyi yanlış girdiniz!");

    }

    public async Task RegisterAsync(RegisterCommand request)
    {
        AppUser user = new()
        {
            Email = request.Email,
            UserName = request.UserName,
            NameLastName = request.NameLastName,
        };

        IdentityResult identityResult = await _userManager.CreateAsync(user, request.Password);
        if (!identityResult.Succeeded)
        {
            throw new Exception(identityResult.Errors.First().Description);
        }
    }
}
