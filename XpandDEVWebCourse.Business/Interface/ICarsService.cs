using FluentResults;
using System.Collections.Generic;
using System.Threading.Tasks;
using XpandDEVWebCourse.Models;

namespace XpandDEVWebCourse.Business
{
    public interface ICarsService
    {
        Task<List<Car>> GetAllCarsAsync();
        Task<Result<Car>> GetCarAsync(int Id);
        Task<Result> NewCarAsync(Car car);
        Task<Result> DeleteCarAsync(int id);
        Task<Car> EditCarAsync(int id);
        Task<Result> UpdateCarAsync(Car car);
    }
}