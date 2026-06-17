using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    /// <summary>
    /// DTO class used as return type for most of Countries Service methods
    /// </summary>
    public class CountryResponse
    {
        public Guid CountryID { get; set; }
        public string? CountryName { get; set; }


        // overriding this to solve - objA.equals(objB)
        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }

            if(obj.GetType() != typeof(CountryResponse))
            {
                return false;
            }

            CountryResponse countryToCompare = (CountryResponse)obj;

            return this.CountryID == countryToCompare.CountryID && this.CountryName == countryToCompare.CountryName;
        }
    }



    // This method will get added to Country class
    // this Country country
    public static class CountryExtensions
    {
        public static CountryResponse ToCountryResponse(this Country country)
        {
            return new CountryResponse()
            {
                CountryID = country.CountryID,
                CountryName = country.CountryName,
            };
        }
    }
}