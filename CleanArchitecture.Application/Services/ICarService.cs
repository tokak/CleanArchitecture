using CleanArchitecture.Application.Features.CarFeatures.Commands.CreateCar;

namespace CleanArchitecture.Application.Services;

public interface ICarService
{

    Task CreateAsync(CreateCarCommand request, CancellationToken cancellationToken);
    /*
    * CreateCarCommand request: 
    * - Araba oluşturma işlemi için gerekli verileri içeren bir komut nesnesidir.
    * - Genellikle aracın marka, model, yıl gibi özelliklerini içerir.
    * - CQRS (Command Query Responsibility Segregation) tasarım deseninde bir "Command" (komut) nesnesi olarak kullanılır.
    * 
    * CancellationToken cancellationToken:
    * - Asenkron işlemi iptal etmek için kullanılan bir belirteçtir.
    * - Eğer işlem uzun sürerse ve iptal edilmesi gerekirse, bu belirteç kullanılarak işlem sonlandırılabilir.
    * - Performans ve kaynak yönetimi açısından önemlidir.  
    */

    // Burada araba oluşturma işlemi gerçekleştirilecek.
}
