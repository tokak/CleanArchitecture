using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.CarFeatures.Commands.CreateCar
{
    public sealed record CreateCarCommandResponse( string Message = "Araç Kaydı başarı ile tamamlandı")
    {
    }
}
