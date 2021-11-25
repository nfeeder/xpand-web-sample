using FluentResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XpandDEVWebCourse.Data;
using XpandDEVWebCourse.Models;

namespace XpandDEVWebCourse.Business
{
    public class CarsService : ICarsService
    {
        private readonly CourseDbContext _dbContext;

        public CarsService(CourseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Models.Car>> GetAllCarsAsync()
        {
            var dbCars = await _dbContext.Cars.ToListAsync();

            var cars = dbCars
                .Select(m => new Models.Car() { Id = m.Id, Model = m.Model, ExternalId = m.ExternalId, NrBolts = m.NrBolts })
                .ToList();

            return cars;
        }

        public async Task<Result<Models.Car>> GetCarAsync(int Id)
        {
            var car = await _dbContext.Cars.FirstOrDefaultAsync(m => m.Id == Id);

            if (car == null)
                return Result.Fail("Error while trying to get card");

            var dtoCar = new Models.Car()
            {
                Id = car.Id,
                Model = car.Model,
                ExternalId = car.ExternalId,
                NrBolts = car.NrBolts
            };

            return Result.Ok(dtoCar);
        }

        public async Task<Result> NewCarAsync(Car car)
        {
            try
            {
                Cars cars = new Cars
                {
                    Model = car.Model,
                    ExternalId = car.ExternalId,
                    NrBolts = car.NrBolts
                };
               
                _dbContext.Add(cars);
                await _dbContext.SaveChangesAsync();
                Console.WriteLine(Result.Ok());
                return Result.Ok();
            }
            catch(Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public async Task<Result> DeleteCarAsync(int id)
        {
            try
            {
                var data = _dbContext.Cars.FirstOrDefault(x => x.Id == id);
                _dbContext.Remove(data);
                await _dbContext.SaveChangesAsync();
                Console.WriteLine(Result.Ok());
                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public async Task<Models.Car> EditCarAsync(int id)
        {
            var car = await _dbContext.Cars.FirstOrDefaultAsync(m => m.Id == id);

            var dtoCar = new Models.Car()
            {
                Id = car.Id,
                Model = car.Model,
                ExternalId = car.ExternalId,
                NrBolts = car.NrBolts
            };

            return (dtoCar);
        }
        public async Task<Result> UpdateCarAsync(Car car)
        {
            try
            {
                Cars cars = new Cars
                {    
                    Id = car.Id,
                    Model = car.Model,
                    ExternalId = car.ExternalId,
                    NrBolts = car.NrBolts
                };

                _dbContext.Update(cars);
                await _dbContext.SaveChangesAsync();
                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }

        }

        
    }
}
