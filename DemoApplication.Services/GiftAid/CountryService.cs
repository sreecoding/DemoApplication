﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoApplication.Repositories;
using DemoApplication.Repositories.Interfaces;
using DemoApplication.Services.GiftAid.Interfaces;

namespace DemoApplication.Services.GiftAid
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;

        public CountryService(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<List<Country>> GetCountryByCountryCode(string countryCode)
        {
            return await _countryRepository.GetCountryByCountryCode(countryCode);
        }
    }
}
