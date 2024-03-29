﻿using HSPAWebAPI.Interfaces;
using HSPAWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

namespace HSPAWebAPI.Data
{
    public class CityRepository : ICityRepository
    {
        private readonly DataContext dc;
        public CityRepository(DataContext dc)
        {
            this.dc = dc;
        }
        public void AddCity(City city)
        {
            dc.Cities.AddAsync(city);
        }

        public async void DeleteCity(int CityId)
        {
            var city =await dc.Cities.FindAsync(CityId);
            dc.Cities.Remove(city);
        }

        public async Task<City> FindCity(int id)
        {
            return await dc.Cities.FindAsync(id);  
        }

        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await dc.Cities.ToListAsync();

        }
    }
}
