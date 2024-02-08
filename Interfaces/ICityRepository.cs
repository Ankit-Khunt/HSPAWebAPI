using HSPAWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace HSPAWebAPI.Interfaces
{
    public interface ICityRepository
    {
        Task<IEnumerable<City>> GetCitiesAsync();

        void AddCity(City city);

        void DeleteCity(int Cityid);

    }
}
