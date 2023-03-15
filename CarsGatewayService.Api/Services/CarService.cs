using AutoMapper;
using Cars.Api.Entities;
using Cars.Api.Models.Cars;
using Cars.Api.Repositories.Interfaces;
using Cars.Api.Services.Interfaces;

namespace Cars.Api.Services;

public class CarService : ICarService
{
    private readonly ICarRepository _carRepository;
    private readonly IMapper _mapper;

    public CarService(ICarRepository carRepository,
        IMapper mapper)
    {
        _carRepository = carRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Car>> GetCars(GetCarsFilter filter)
    {
        return await _carRepository.GetCars(filter.Offset, filter.Count);
    }

    public async Task<Car> GetById(int id)
    {
        var car =  await _carRepository.GetById(id);
        if (car == null)
            throw new KeyNotFoundException("Car not found");

        return car;
    }

    public async Task<int> Create(CreateCarRequest model, int userId)
    {
        var car = _mapper.Map<Car>(model);
        car.UserId = userId;
        
        return await _carRepository.Create(car);
    }

    public async Task Update(int id, UpdateCarRequest model)
    {
        var car = await _carRepository.GetById(id);

        if (car == null)
            throw new KeyNotFoundException("Car not found");
        
        _mapper.Map(model, car);
        
        await _carRepository.Update(car);
    }

    public async Task Delete(int id)
    {
        await _carRepository.Delete(id);
    }
}