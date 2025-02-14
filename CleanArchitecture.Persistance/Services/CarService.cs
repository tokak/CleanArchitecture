using AutoMapper;
using CleanArchitecture.Application.Features.CarFeatures.Commands.CreateCar;
using CleanArchitecture.Application.Features.CarFeatures.Queries.GetAllCar;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Persistance.Context;
using GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Persistance.Services
{
    public sealed class CarService : ICarService
    {
        private readonly AppDbContext _context;
        private readonly ICarRepository _carRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CarService(AppDbContext context, IMapper mapper, ICarRepository carRepository, IUnitOfWork unitOfWork)
        {
            _context = context;
            _mapper = mapper;
            _carRepository = carRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateAsync(CreateCarCommand request, CancellationToken cancellationToken)
        {
            //Car car = new()
            //{
            //    Name = request.Name,
            //    Model = request.Model,
            //    EnginePower = request.EnginePower
            //};
            Car car = _mapper.Map<Car>(request);
            //await _context.Set<Car>().AddAsync(car,cancellationToken);
            //await _context.SaveChangesAsync(cancellationToken);

            await _carRepository.AddAsync(car, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<Pagination.IPage<Car>> GetAllAsync(GetAllQuery request, CancellationToken cancellationToken)
        {
            //       Pagination.IPage<Car> cars = await _carRepository
            //.GetAll()
            //.Where(x => string.IsNullOrEmpty(request.Search) || x.Name.ToLower().Contains(request.Search.ToLower()))
            //.ToListAsync(request.PageNumber,request.PageSize,cancellationToken).;
            //       return cars;
            //   }

            return null;
        }
    }
}
