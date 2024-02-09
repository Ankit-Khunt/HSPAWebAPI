using AutoMapper;
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
        private readonly IUnitOfWork uow;

        public IMapper mapper { get; }

        public CityController(IUnitOfWork umo, IMapper mapper)
        {
            this.uow = umo;
            this.mapper = mapper;
        }

        [HttpGet("cities")]
        public async Task<IActionResult> GetCities()
        {
            var cities = await uow.CityRepository.GetCitiesAsync();

            var citiesDto = mapper.Map<IEnumerable<CityDto>>(cities);

            //var cityDto = from c in cities
            //              select new CityDto()
            //              {
            //                  id = c.id,
            //                  Name = c.Name
            //              };

            throw new Exception("Get method exception");

            return Ok(citiesDto);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddCity(CityDto cityDto)
        {
            var city = mapper.Map<City>(cityDto);

            city.LastUpdatedBy = 1;
            city.LastUpdatedOn
                = DateTime.Now;

            uow.CityRepository.AddCity(city);
            await uow.SaveAsync();
            return StatusCode(201);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCity(int id, CityDto cityDto)
        {
            var cityFromDb = await uow.CityRepository.FindCity(id);
            mapper.Map(cityDto, cityFromDb);
            cityFromDb.LastUpdatedOn = DateTime.Now;
            cityFromDb.LastUpdatedBy = 1;
            await uow.SaveAsync();
            return StatusCode(200);
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
            uow.CityRepository.DeleteCity(id);

            await uow.SaveAsync();

            return Ok(id);
        }
    }
}
