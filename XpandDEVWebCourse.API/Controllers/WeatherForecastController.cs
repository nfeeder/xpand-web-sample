using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using XpandDEVWebCourse.Business;



namespace XpandDEVWebCourse.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ICarsService _carsService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ICarsService carsService)
        {
            _logger = logger;
            _carsService = carsService;
        }

       
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        
        [HttpDelete]
        [Route("api_DeleteCar")]
        public bool DeleteCar(int id)
        {
            try
            {
                _carsService.DeleteCarAsync(id);
                return true;
            }
            catch 
            {
                return false;
            }

        }
    }
}
