using HSPAWebAPI.Dtos;
using HSPAWebAPI.Interfaces;
using HSPAWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace HSPAWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly IUnitOfWork umo;
        public CityController(ICityRepository repo,IUnitOfWork umo)
        {
            this.umo = umo;
        }

        [HttpGet("cities")]
        public async Task<IActionResult> GetCities()
        {
            var cities = await umo.CityRepository.GetCitiesAsync();

            var cityDto = from c in cities
                          select new CityDto()
                          {
                              id = c.id,
                              Name = c.Name
                          };

            return Ok(cityDto);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddCity(CityDto cityDto)
        {
            // mapping
            var city = new City
            {
                Name = cityDto.Name,
                LastUpdatedBy = 1,
                LastUpdatedOn = DateTime.Now,
            };

            umo.CityRepository.AddCity(city);
            await umo.SaveAsync();
            return StatusCode(201);
        }

        //[HttpDelete("delete/{id}")]
        //public async Task<IActionResult> DeleteCity(int id)
        //{
        //    var city = await dc.Cities.FindAsync(id);
        //    dc.Cities.Remove(city);
        //    await dc.SaveChangesAsync();
        //    return Ok(id);
        //}

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            // var city = await dc.Cities.FindAsync(id);
            umo.CityRepository.DeleteCity(id);

            await umo.SaveAsync();

            return Ok(id);
        }
    }
}
