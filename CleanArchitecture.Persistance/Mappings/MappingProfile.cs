using AutoMapper;
using CleanArchitecture.Application.Features.CarFeatures.Commands.CreateCar;

namespace CleanArchitecture.Persistance.Mappings;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateCarCommand, CreateCarCommand>().ReverseMap();
    }
}
