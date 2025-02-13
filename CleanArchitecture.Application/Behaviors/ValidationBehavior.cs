using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace CleanArchitecture.Application.Behaviors;

/// <summary>
/// Bu sınıf, MediatR pipeline'ında çalışan bir validation (doğrulama) davranışını temsil eder.
/// TRequest tipindeki her bir istek için, ilgili validatörleri çalıştırır ve doğrulama hatalarını kontrol eder.
/// Eğer doğrulama hataları varsa, bu hataları bir istisna (exception) olarak fırlatır.
/// Doğrulama hatası yoksa, bir sonraki pipeline adımına geçer.
/// </summary>
public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : class, IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    /// <summary>
    /// Bu metod, MediatR pipeline'ında gelen isteği (request) işler.
    /// İlk olarak, ilgili validatörlerin olup olmadığını kontrol eder. Eğer validatör yoksa, bir sonraki adıma geçer.
    /// Validatörler varsa, isteği doğrular ve hataları toplar.
    /// Eğer doğrulama hataları varsa, bu hataları bir ValidationException olarak fırlatır.
    /// Doğrulama hatası yoksa, bir sonraki pipeline adımına geçer.
    /// </summary>
    /// <param name="request">Gelen istek (request) nesnesi.</param>
    /// <param name="next">Bir sonraki pipeline adımını temsil eden delegedir.</param>
    /// <param name="cancellationToken">İşlemin iptal edilmesini sağlayan token.</param>
    /// <returns>Doğrulama başarılıysa, bir sonraki adımın sonucunu döner.</returns>
    /// <exception cref="ValidationException">Doğrulama hataları varsa fırlatılır.</exception>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        // Eğer hiç validatör yoksa, bir sonraki adıma geç.
        if (!_validators.Any())
        {
            return await next();
        }

        // FluentValidation için bir doğrulama context'i oluştur.
        var context = new ValidationContext<TRequest>(request);

        // Tüm validatörleri çalıştır ve hataları topla.
        var errorDictionary = _validators
            .Select(s => s.Validate(context))
            .SelectMany(s => s.Errors)
            .Where(w => w != null)
            .GroupBy(
            g => g.PropertyName,
            g => g.ErrorMessage, (propertyName, errorMessage) => new
            {
                Key = propertyName,
                Values = errorMessage.Distinct().ToArray()
            })
            .ToDictionary(s => s.Key, s => s.Values[0]);

        // Eğer doğrulama hataları varsa, bir ValidationException fırlat.
        if (errorDictionary.Any())
        {
            var errors = errorDictionary.Select(s => new ValidationFailure
            {
                PropertyName = s.Value,
                ErrorCode = s.Key
            });
            throw new ValidationException(errors);
        }

        // Doğrulama hatası yoksa, bir sonraki adıma geç.
        return await next();
    }
}