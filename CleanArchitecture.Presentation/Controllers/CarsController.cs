﻿using CleanArchitecture.Application.Features.CarFeatures.Commands.CreateCar;
using CleanArchitecture.Application.Features.CarFeatures.Queries.GetAllCar;
using CleanArchitecture.Domain.Dtos;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Presentation.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Presentation.Controllers;

public sealed class CarsController : ApiController
{
    public CarsController(IMediator mediator) : base(mediator) { }

    [HttpPost("[action]")]
    public async Task<IActionResult> CreateCar(CreateCarCommand request, CancellationToken cancellationToken)
    {
        MessageResponse messageResponse = await _mediator.Send(request, cancellationToken);
        return Ok(messageResponse);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> GetAll(GetAllQuery request, CancellationToken cancellationToken)
    {
        
        Pagination.IPage<Car> response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }
}
