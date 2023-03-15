using Cars.Api.Entities;

namespace Cars.Api.Repositories.Interfaces;

public interface ICarRepository
{
    Task<IEnumerable<Car>> GetCars(int offset, int limit);
    Task<Car> GetById(int id);
    Task<int> Create(Car car);
    Task Update(Car car);
    Task Delete(int id);
}