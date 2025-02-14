using FluentValidation;

namespace CleanArchitecture.Application.Features.AuthFeatures.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(p => p.Email).NotEmpty().WithMessage("Mail bilgisi boş olamaz");
        RuleFor(p => p.Email).NotNull().WithMessage("Mail bilgisi boş olamaz");
        RuleFor(p => p.Email).EmailAddress().WithMessage("Geçerli mail adresi giriniz");

        RuleFor(p => p.UserName).NotEmpty().WithMessage("Kullanıcı adı boş olamaz");
        RuleFor(p => p.UserName).NotEmpty().WithMessage("Kullanıcı adı boş olamaz");
        RuleFor(p => p.UserName).MinimumLength(3).WithMessage("Kullanıcı adı en az 3 karekter olmalıdır.");

        RuleFor(p => p.Password).NotEmpty().WithMessage("Şifre boş olamaz");
        RuleFor(p => p.Password).NotNull().WithMessage("Şifre boş olamaz");
        RuleFor(p => p.Password).Matches("[A-Z]").WithMessage("Şifre en az bir adet büyük harf içermelidir");
        RuleFor(p => p.Password).Matches("[a-z]").WithMessage("Şifre en az bir adet küçük harf içermelidir");
        RuleFor(p => p.Password).Matches("[0-9]").WithMessage("Şifre en az bir adet rakam içermelidir");
        RuleFor(p => p.Password).Matches("[^a-zA-z0-9]").WithMessage("Şifre en az bir adet özel karekter içermelidir");
    }
}
