using CleanArchitecture.Application.Features.CarFeatures.Commands.CreateCar;
using CleanArchitecture.Domain.Dtos;
using CleanArchitecture.Presentation.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CleanArchitecture.UnitTest;

public class CarsControllerUnitTest
{
    [Fact]
    public async void Create_ReturnsOkResult_WhenRequestIsValid()
    {

        //Arrange(Haz�rl�k a�amas�):Test i�in gerekli ba��ml�l�klar ve mock (sahte (fake) nesnelerdir) nesneleri burada olu�turulmal�.
        var mediatorMock = new Mock<IMediator>();
        CreateCarCommand createCarCommand = new("Toyota","Corolla",5000);
        MessageResponse messageResponse = new("Ara� ba�ar�l� bir �ekilde kaydedildi.");
        CancellationToken cancellationToken = new();

        mediatorMock.Setup(x => x.Send(createCarCommand, cancellationToken)).ReturnsAsync(messageResponse);

        CarsController carsController = new(mediatorMock.Object);

        //Act(Eylem a�amas�):Create metodunu �a��r�p sonucunu almak gerekiyor.
        var result  = await carsController.CreateCar(createCarCommand,cancellationToken);


        //Assert(Do�rulama a�amas�): D�nen sonucun OK(200) olup olmad���n� do�rulamak gerekiyor.
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<MessageResponse>(okResult.Value);

        Assert.Equal(messageResponse.Message, returnValue.Message); 
        mediatorMock.Verify(x => x.Send(createCarCommand, cancellationToken), Times.Once);
    }
}