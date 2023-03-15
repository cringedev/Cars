using Cars.Api.Entities;
using Cars.Api.Models.Cars;

namespace Cars.Api.Services.Interfaces;

public interface ICarService
{
    Task<IEnumerable<Car>> GetCars(GetCarsFilter filter);
    Task<Car> GetById(int id);
    Task<int> Create(CreateCarRequest model, int userId);
    Task Update(int id, UpdateCarRequest model);
    Task Delete(int id);
}