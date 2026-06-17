using Entities;
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

        public CountriesService()
        {
            _countries = new List<Country>()
            {
                new Country() { CountryID = Guid.NewGuid(), CountryName = "Japan" },
                new Country() { CountryID = Guid.NewGuid(), CountryName = "Korea" },
                new Country() { CountryID = Guid.NewGuid(), CountryName = "China" },
                new Country() { CountryID = Guid.NewGuid(), CountryName = "UBA" },
            };
        }

        public CountryResponse AddCountry(CountryAddRequest? countryAddRequest)
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
            if(_countries.Where(temp => temp.CountryName == countryAddRequest.CountryName).Count() > 0)
            {
                throw new ArgumentException("Given country name already exists");
            }

            // Convert object from CountryAddRequest to Country type
            Country country = countryAddRequest.ToCountry();

            // generate CountryID
            country.CountryID = Guid.NewGuid();

            // Add country object into _countries
            _countries.Add(country);

            return country.ToCountryResponse();
        }

        public List<CountryResponse> GetAllCountries()
        {
            List<CountryResponse> result = _countries.Select(country => country.ToCountryResponse()).ToList();
            return result;
        }


        public CountryResponse? GetCountryByCountryID(Guid? countryID)
        {
            if (countryID == null)
            {
                return null;
            }

            Country? country_response_from_list = _countries?.FirstOrDefault(c => c.CountryID == countryID);

            if (country_response_from_list == null)
            {
                return null;
            }

            return country_response_from_list.ToCountryResponse();
        }
    }
}
