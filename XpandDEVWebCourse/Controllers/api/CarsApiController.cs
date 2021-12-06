using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XpandDEVWebCourse.Business;
using XpandDEVWebCourse.Web.ViewModels;
using XpandDEVWebCourse.Models;

namespace XpandDEVWebCourse.Web.Controllers.api
{
    [ApiController]
    [Route("api/cars")]
    public class CarsApiController : Controller
    {
        private readonly ICarsService _carsService;

        public CarsApiController(ICarsService carsService)
        {
            _carsService = carsService;
        }

        [HttpGet]
        [Route("DeleteCar")]
        public async Task<IActionResult> DeleteCars(int id)
        {

            var cars = await _carsService.DeleteCarAsync(id);

            var result = _carsService.GetAllCarsAsync();

            // return PartialView("_ListedCar", result);
            if (cars.IsSuccess)
                return Ok();
            return BadRequest();

        }

        [HttpPost]
        [Route("NewCar")]
        public async Task<IActionResult> NewCars(CarViewModel car)
        {
            var carDto = new Car()
            {
                Model = car.Model,
                ExternalId = car.ExternalId,
                NrBolts = car.NrBolts
            };

            var cars = await _carsService.NewCarAsync(carDto);

            var result = await _carsService.GetAllCarsAsync();

            //return PartialView("_ListedCar", result);
            if (cars.IsSuccess)
                return Ok();
            return BadRequest();
        }

        [HttpGet]
        [Route("UpdateCar")]
        public async Task<IActionResult> UpdateCars(int id, string model, int nrBolts, int externalId)
        {
            var carDto = new Car()
            {
                Id = id,
                Model = model,
                ExternalId = externalId,
                NrBolts = nrBolts
            };

            var cars = await _carsService.UpdateCarAsync(carDto);
            //var result = await _carsService.GetAllCarsAsync();
            //return PartialView("_ListedCar", result);
            if (cars.IsSuccess)
                return Ok();
            return BadRequest();
        }
        }
}
