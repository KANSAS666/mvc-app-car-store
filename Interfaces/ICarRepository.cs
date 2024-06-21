using CarStore.Models;

namespace CarStore.Interfaces
{
    public interface ICarRepository
    {
        Task<Guid> Create(Car carModel);
        Task<Guid> Delete(Guid id);
        Task<List<Car>> Get();
        Task<Car> GetById(Guid id);
        Task<Guid> Update(UpdateCar carModel);
    }
}