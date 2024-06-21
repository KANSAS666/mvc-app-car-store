using Microsoft.AspNetCore.Mvc;
using CarStore.Models;
using CarStore.Interfaces;

namespace CarStore.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarRepository _carRepository;
        private IWebHostEnvironment _webHostEnvironment;

        public CarController(IWebHostEnvironment webHostEnvironment, ICarRepository carRepository)
        {
            _webHostEnvironment = webHostEnvironment;
            _carRepository = carRepository;
        }

        public async Task<IActionResult> Index()
        {
            var allCars = await _carRepository.Get();
            return View(allCars);
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> CarDetails(Guid id)
        {
            var carEntity = await _carRepository.GetById(id);
            var updateCarModel = new UpdateCar
            {
                Id = carEntity.Id,
                Mark = carEntity.Mark,
                Model = carEntity.Model,
                Mileage = carEntity.Mileage,
                Color = carEntity.Color,
                ImagePath = carEntity.ImagePath,
                ExistingImagePath = carEntity.ImagePath
            };

            return View(updateCarModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNewCar(Car carModel)
        {
            if (ModelState.IsValid)
            {
                if (carModel.Image != null)
                {
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + carModel.Image.FileName;
                    string folder = "img/";
                    folder += uniqueFileName;

                    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, folder);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await carModel.Image.CopyToAsync(fileStream);
                    }

                    carModel.ImagePath = "/" + folder;

                    await _carRepository.Create(carModel);

                    return RedirectToAction("Index");
                }
            }
            return View("Create", carModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateCar carModel)
        {
            if (ModelState.IsValid)
            {
                if (carModel.Image != null)
                {
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + carModel.Image.FileName;
                    string folder = "img/";
                    folder += uniqueFileName;

                    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, folder);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await carModel.Image.CopyToAsync(fileStream);
                    }

                    string oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, carModel.ExistingImagePath.TrimStart('/'));
                    if (System.IO.File.Exists(oldImagePath))
                        System.IO.File.Delete(oldImagePath);

                    carModel.ImagePath = "/" + folder;              
                }
                else
                {
                    carModel.ImagePath = carModel.ExistingImagePath;
                }
  
                await _carRepository.Update(carModel);
                return RedirectToAction("Index");
            }

            return View("CarDetails", carModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id, string imagePath)
        {
            string fullImagePath = Path.Combine(_webHostEnvironment.WebRootPath, imagePath.TrimStart('/'));

            if(System.IO.File.Exists(fullImagePath))
            {
                System.IO.File.Delete(fullImagePath);
            }

            await _carRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}

