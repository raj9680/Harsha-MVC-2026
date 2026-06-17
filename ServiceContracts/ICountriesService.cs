using ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    /// <summary>
    /// Represents the business logic for manipulating Country entity
    /// </summary>
    public interface ICountriesService
    {
        /// <summary>
        /// Adds country object to the list of countries
        /// </summary>
        /// <param name="countryAddRequest">Country Object to add</param>
        /// <returns>Returns the country object after adding it (including new generated country)</returns>
        CountryResponse AddCountry(CountryAddRequest? countryAddRequest);

        /// <summary>
        /// Return List of Countries 
        /// </summary>
        /// <returns></returns>
        List<CountryResponse> GetAllCountries();

        /// <summary>
        /// Returns a country object based on the given country id
        /// </summary>
        /// <param name="countryID">Guid - CountryID to search</param>
        /// <returns>Country Response or Null</returns>
        CountryResponse? GetCountryByCountryID(Guid? countryID);
    }
}
