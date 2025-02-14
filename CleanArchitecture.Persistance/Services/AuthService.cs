using AutoMapper;
using CleanArchitecture.Application.Features.AuthFeatures.Commands.Register;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Persistance.Services;

public sealed class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;
    public AuthService(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task RegisterAsync(RegisterCommand request)
    {
        AppUser user = new()
        {
            Email = request.Email,
            UserName = request.UserName,
            NameLastName = request.NameLastName,
        };

        IdentityResult identityResult =  await _userManager.CreateAsync(user, request.Password);
        if (!identityResult.Succeeded)
        {
            throw new Exception(identityResult.Errors.First().Description);
        }
    }
}
