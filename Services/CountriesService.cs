using Entities;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CountriesService : ICountriesService
    {
        private readonly List<Country> _countries = null;
        private readonly PersonsDbContext _dbContext;

        public CountriesService()
        {
            
        }
        public CountriesService(PersonsDbContext personsDbContext, bool initialize = true)
        {
            _dbContext = personsDbContext;
        }

        public async Task<CountryResponse> AddCountry(CountryAddRequest? countryAddRequest)
        {
            // Validation: countryAddRequest parameter can't be null
            if(countryAddRequest == null)
            {
                throw new ArgumentNullException(nameof(countryAddRequest));
            }

            // Validation: countryName can't be blank
            if(countryAddRequest.CountryName == null)
            {
                throw new ArgumentException(nameof(countryAddRequest.CountryName));
            }

            // Validation: Country name can't be duplicate
            if(await _dbContext.Countries.CountAsync(temp => temp.CountryName == countryAddRequest.CountryName) > 0)
            {
                throw new ArgumentException("Given country name already exists");
            }

            // Convert object from CountryAddRequest to Country type
            Country country = countryAddRequest.ToCountry();

            // generate CountryID
            country.CountryID = Guid.NewGuid();

            // Add country object into _countries
            _dbContext.Countries.Add(country);
            await _dbContext.SaveChangesAsync();

            return country.ToCountryResponse();
        }

        public async Task<List<CountryResponse>> GetAllCountries()
        {
            List<CountryResponse> result = await _dbContext.Countries.Select(country => country.ToCountryResponse()).ToListAsync();
            return result;
        }


        public async Task<CountryResponse>? GetCountryByCountryID(Guid? countryID)
        {
            if (countryID == null)
            {
                return null;
            }

            Country? country_response_from_list = await _dbContext.Countries.FirstOrDefaultAsync(c => c.CountryID == countryID);

            if (country_response_from_list == null)
            {
                return null;
            }

            return country_response_from_list.ToCountryResponse();
        }
    }
}
