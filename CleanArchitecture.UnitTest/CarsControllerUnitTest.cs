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

        //Arrange(Hazýrlýk aþamasý):Test için gerekli baðýmlýlýklar ve mock (sahte (fake) nesnelerdir) nesneleri burada oluþturulmalý.
        var mediatorMock = new Mock<IMediator>();
        CreateCarCommand createCarCommand = new("Toyota","Corolla",5000);
        MessageResponse messageResponse = new("Araç baþarýlý bir þekilde kaydedildi.");
        CancellationToken cancellationToken = new();

        mediatorMock.Setup(x => x.Send(createCarCommand, cancellationToken)).ReturnsAsync(messageResponse);

        CarsController carsController = new(mediatorMock.Object);

        //Act(Eylem aþamasý):Create metodunu çaðýrýp sonucunu almak gerekiyor.
        var result  = await carsController.CreateCar(createCarCommand,cancellationToken);


        //Assert(Doðrulama aþamasý): Dönen sonucun OK(200) olup olmadýðýný doðrulamak gerekiyor.
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<MessageResponse>(okResult.Value);

        Assert.Equal(messageResponse.Message, returnValue.Message); 
        mediatorMock.Verify(x => x.Send(createCarCommand, cancellationToken), Times.Once);
    }
}