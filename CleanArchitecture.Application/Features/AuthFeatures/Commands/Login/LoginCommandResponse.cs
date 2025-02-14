namespace CleanArchitecture.Application.Features.AuthFeatures.Commands.Login;

public sealed record LoginCommandResponse(
    string Token, 
    string RefreshToken,
    DateTime? RefreshTokenExpiryTime,
    string UserId,
    string Email);

