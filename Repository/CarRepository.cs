using CarStore.Data;
using CarStore.Interfaces;
using CarStore.Models;
using Microsoft.EntityFrameworkCore;
namespace CarStore.Repository
{
    public class CarRepository : ICarRepository
    {
        private readonly CarDbContext _context;

        public CarRepository(CarDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Create(Car carModel)
        {
            if (carModel.Color is null)
                carModel.Color = "Not";

            var carEntity = new CarEntity
            {
                Id = Guid.NewGuid(),
                Mark = carModel.Mark,
                Model = carModel.Model,
                Mileage = carModel.Mileage,
                Color = carModel.Color,
                ImagePath = carModel.ImagePath
            };

            await _context.Cars.AddAsync(carEntity);
            await _context.SaveChangesAsync();

            return carEntity.Id;
        }

        public async Task<List<Car>> Get()
        {
            var carEntities = await _context.Cars.ToListAsync();

            var cars = carEntities.Select(c => new Car
            {
                Id = c.Id,
                Mark = c.Mark,
                Model = c.Model,
                Mileage = c.Mileage,
                Color = c.Color,
                ImagePath = c.ImagePath
            }).ToList();

            return cars;

        }

        public async Task<Car> GetById(Guid id)
        {
            var carEntity = await _context.Cars.FindAsync(id);
            var carModel = new Car
            {
                Id = carEntity.Id,
                Mark = carEntity.Mark,
                Model = carEntity.Model,
                Mileage = carEntity.Mileage,
                Color = carEntity.Color,
                ImagePath = carEntity.ImagePath
            };

            return carModel;
        }

        public async Task<Guid> Update(UpdateCar carModel)
        {
            var carEntityToUpdate = await _context.Cars.FindAsync(carModel.Id);
            if (carEntityToUpdate == null)
            {
                throw new Exception("Not found");
            }

            carEntityToUpdate.Mark = carModel.Mark;
            carEntityToUpdate.Model = carModel.Model;
            carEntityToUpdate.Mileage = carModel.Mileage;
            carEntityToUpdate.Color = carModel.Color;
            carEntityToUpdate.ImagePath = carModel.ImagePath;
            

            await _context.SaveChangesAsync();

            return carEntityToUpdate.Id;

        }

        public async Task<Guid> Delete(Guid id)
        {
            await _context.Cars.Where(c => c.Id == id).ExecuteDeleteAsync();
            await _context.SaveChangesAsync();

            return id;
        }
    }
}
