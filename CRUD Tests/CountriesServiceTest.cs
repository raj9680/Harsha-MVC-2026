using ServiceContracts;
using ServiceContracts.DTO;
using Services;
using Xunit;

namespace CRUD_Test
{
    public class CountriesServiceTest
    {
        private readonly ICountriesService _countriesService;

        public CountriesServiceTest()
        {
            _countriesService = new CountriesService();
        }


        // When CountryAddRequest is null, it should throw ArgumentNullException
        [Fact]
        public void AddCountry_NullCountry()
        {
            // Arrange - 1
            CountryAddRequest? request = null;

            // Assert - 3
            Assert.Throws<ArgumentNullException>(() =>
            {
                // Act - 2
                _countriesService.AddCountry(request);
            });
        }


        // When the CountryName is null, it should throw ArgumentException
        [Fact]
        public void AddCountry_CountryNameIsNull()
        {
            // Arrange
            CountryAddRequest? request = new CountryAddRequest()
            {
                CountryName = null
            };

            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Act
                _countriesService.AddCountry(request);
            });
        }


        // When the Country is duplicate, it should through ArgumentException
        [Fact]
        public void AddCountry_DuplicateCountryName()
        {
            // Arrange
            CountryAddRequest? request1 = new CountryAddRequest()
            {
                CountryName = "USA1"
            };

            CountryAddRequest? request2 = new CountryAddRequest()
            {
                CountryName = "USA1"
            };

            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Act
                _countriesService.AddCountry(request1);
                _countriesService.AddCountry(request2);
            });
        }


        // When you supply proper country name, it should insert (add) the country
        // to the existing list of countries.
        [Fact]
        public void AddCountry_ProperCountryDetails()
        {
            // Arrange
            CountryAddRequest? request = new CountryAddRequest()
            {
                CountryName = "Russia"
            };

            // Act 
            CountryResponse response = _countriesService.AddCountry(request);

            List<CountryResponse> countriesFromGetAllCountries = _countriesService.GetAllCountries();

            // Assert
            Assert.True(response.CountryID != Guid.Empty);

            // It compares the ref. not values i.e objA.equals(objB)
            Assert.Contains(response, countriesFromGetAllCountries); // so we need to override 
        }


        [Fact]
        public void GetAllCountries_AddFewCountries()
        {
            // Arrange
            List<CountryAddRequest> listCountries = new List<CountryAddRequest>()
            {
                new CountryAddRequest() { CountryName = "India" },
                new CountryAddRequest() { CountryName = "Sri Lanka" }
            };

            // To Adding response after add
            List<CountryResponse> countryResponse = new List<CountryResponse>();

            // Act
            foreach (var countryItem in listCountries)
            {
                countryResponse.Add(_countriesService.AddCountry(countryItem));
            }

            List<CountryResponse> actualCountryResponse = _countriesService.GetAllCountries();

            // read each element from list of countries response
            foreach (CountryResponse expectedCountry in countryResponse)
            {
                Assert.Contains(expectedCountry, actualCountryResponse);
            }
        }


        // List of countries should be empty
        [Fact]
        public void GetAllCountries_EmptyList()
        {
            // Act
            List<CountryResponse> countriesList = _countriesService.GetAllCountries();

            // clear the list
            countriesList.Clear();

            // Assert
            Assert.Empty(countriesList);
        }


        // List of countries should be not empty
        [Fact]
        public void GetAllCountry_NotEmpty()
        {
            List<CountryResponse> countriesList = _countriesService.GetAllCountries();

            Assert.NotEmpty(countriesList);
        }


        // Get CountryByCountryID - If we supply null as CountryID, it should return
        // null as CountryResponse
        [Fact]
        public void GetCountryByCountryID_NullCountryID()
        {
            // Arrange
            Guid? countryID = null;
            CountryResponse? country = _countriesService.GetCountryByCountryID(countryID);

            Assert.Null(country);
        }


        // If we supply a valid country id; it should return the matching country
        // details as CountryResponse object
        [Fact]
        public void GetCountryByCountryID_ValidCountryID()
        {
            // Arrange
            CountryAddRequest? countryRequest = new CountryAddRequest()
            {
                CountryName = "Australia"
            };

            // Add
            CountryResponse countryfromAdd = _countriesService.AddCountry(countryRequest);

            // Get
            CountryResponse? countryFromGet = _countriesService.GetCountryByCountryID(countryfromAdd.CountryID);

            Assert.Equal(countryfromAdd, countryFromGet);
        }
    }
}
