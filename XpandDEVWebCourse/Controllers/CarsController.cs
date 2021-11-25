using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XpandDEVWebCourse.Business;
using XpandDEVWebCourse.Web.ViewModels;
using XpandDEVWebCourse.Data;
using XpandDEVWebCourse.Models;



namespace XpandDEVWebCourse.Web.Controllers
{
    [Route("Cars")]
    public class CarsController : Controller
    {
        private readonly ICarsService _carsService;

        public CarsController(ICarsService carsService)
        {
            _carsService = carsService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var cars = await _carsService.GetAllCarsAsync();

            var carResult = await _carsService.GetCarAsync(1);

            if (carResult.IsFailed)
                return BadRequest();

            var carsVm = cars
            .Select(m => new CarViewModel()
            {
                Id = m.Id,
                Model = m.Model,
                ExternalId = m.ExternalId,
                NrBolts = m.NrBolts
            }).ToList();

            return View(carsVm);
        }

        [HttpPost]
        [Route("new-car")]
        public async Task<IActionResult> NewCar(CarViewModel car)
        {
            var carDto = new Car()
            {
                Model = car.Model,
                ExternalId = car.ExternalId,
                NrBolts = car.NrBolts
            };

            var cars = await _carsService.NewCarAsync(carDto);

            var result = await _carsService.GetAllCarsAsync();

            if (cars.IsFailed)
            {
                ModelState.TryAddModelError("FailMessage", "Failed to add car!");
            }
            else
            {
                ModelState.TryAddModelError("SuccessMessage", "Car created successfully!");
            }

            return RedirectToAction(nameof(CarsController.Index));
        }

        [HttpPost]
        [Route("remove-car")]
        public async Task<IActionResult> RemoveCar(int id)
        {
            var cars = await _carsService.DeleteCarAsync(id);

            var result = await _carsService.GetAllCarsAsync();

            if (cars.IsFailed)
            {
                ModelState.TryAddModelError("FailMessage", "Failed to delete car!");
            }
            else
            {
                ModelState.TryAddModelError("SuccessMessage", "Car deleted successfully!");
            }

          return RedirectToAction(nameof(CarsController.Index));
        }

        [HttpPost]
        [Route("edit-car")]
        public async Task<IActionResult> EditCar(int id)
        {
           
            var carResult = await _carsService.EditCarAsync(id);

            var carV = new CarViewModel()
            {
                Id = carResult.Id,
                ExternalId = carResult.ExternalId,
                Model = carResult.Model,
                NrBolts = carResult.NrBolts
            };

            return View(carV);
        }

        [HttpPost]
        [Route("update-car")]
        public async Task<IActionResult> UpdateCar(CarViewModel car)
        {
            var carDto = new Car()
            {
                Id = car.Id,
                Model = car.Model,
                ExternalId = car.ExternalId,
                NrBolts = car.NrBolts
            };

            var cars = await _carsService.UpdateCarAsync(carDto);

            //var result = await _carsService.GetAllCarsAsync();

            if (cars.IsFailed)
            {
                ModelState.TryAddModelError("FailMessage", "Failed to add car!");
            }
            else
            {
                ModelState.TryAddModelError("SuccessMessage", "Car created successfully!");
            }
            return RedirectToAction(nameof(CarsController.Index));
        }
    }
}
