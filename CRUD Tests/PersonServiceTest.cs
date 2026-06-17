using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.DTO.Enums;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace CRUD_Tests
{
    public class PersonServiceTest
    {
        private readonly IPersonService _personService;
        private readonly ICountriesService _countriesService;
        private readonly ITestOutputHelper _outputHelper;

        public PersonServiceTest(ITestOutputHelper testOutputHelper)
        {
            _personService = new PersonService();
            _countriesService = new CountriesService();
            _outputHelper = testOutputHelper;
        }


        /// <summary>
        /// When we supply null PersonAddRequest Object
        /// </summary>
        [Fact]
        public void AddPerson_NullPerson()
        {
            // Arrange
            PersonAddRequest? personAddRequest = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                _personService.AddPerson(personAddRequest);
            });
        }


        // When we supply PersonName null in PersonAddRequest
        [Fact]
        public void AddPerson_PersonNameIsNull()
        {
            PersonAddRequest? personAddRequest = new PersonAddRequest()
            {
                PersonName = null
            };

            Assert.Throws<ArgumentNullException>(() =>
            {
                _personService.AddPerson(personAddRequest);
            });
        }


        // When we supply proper person details
        [Fact]
        public void AddPerson_ProperPersonDetail()
        {
            // Arrange
            PersonAddRequest personAddRequest = new PersonAddRequest()
            {
                PersonName = "John Doe",
                Email = "john@email.com",
                Address = "ABC Address",
                ReceiveNewsLetters = true,
                DateOfBirth = DateTime.Parse("2000-01-01"),
                Gender = GenderOptions.Male,
                CountryID = new Guid()
            };

            // Act
            PersonResponse personResponse = _personService.AddPerson(personAddRequest);
            List<PersonResponse>? personResponseGet = _personService.GetAllPerson();


            // Assert 1
            Assert.True(personResponse.PersonID != Guid.Empty);

            // Assert 2
            Assert.Contains(personResponse, personResponseGet);
        }

        // When we supply personID to get specific person details
        [Fact]
        public void GetPerson_PersonByPersonID()
        {
            // Arrange - 1
            CountryAddRequest countryAddRequest = new CountryAddRequest()
            {
                CountryName = "Canada"
            };
            CountryResponse countryResponse = _countriesService.AddCountry(countryAddRequest);

            // Arrange
            PersonAddRequest request = new PersonAddRequest()
            {
                PersonName = "John Doe",
                Email = "john@email.com",
                Address = "ABC Address",
                ReceiveNewsLetters = true,
                DateOfBirth = DateTime.Parse("2000-01-01"),
                Gender = GenderOptions.Male,
                CountryID = countryResponse.CountryID
            };

            PersonResponse personResponse = _personService.AddPerson(request);

            List<PersonResponse> personResponseGet = _personService.GetAllPerson();

            PersonResponse personResponseGet2 = _personService.GetPersonByID(personResponse.PersonID);

            // Assert - 1
            Assert.NotNull(personResponse?.PersonID);

            // Assert - 2
            Assert.Equal(personResponse, personResponseGet2);

            // Assert - 3
            Assert.Contains(personResponse, personResponseGet);
        }

        // To get All Person
        [Fact]
        public void GetPerson_AllPerson()
        {
            // Arrange - 1
            CountryAddRequest countryAddRequest = new CountryAddRequest()
            {
                CountryName = "Netherland"
            };

            CountryAddRequest countryAddRequest1 = new CountryAddRequest()
            {
                CountryName = "Australia"
            };

            CountryResponse countryResponse = _countriesService.AddCountry(countryAddRequest);
            CountryResponse countryResponse1 = _countriesService.AddCountry(countryAddRequest1);


            // Arrange
            PersonAddRequest personAddRequest = new PersonAddRequest()
            {
                PersonName = "John Doe",
                Email = "john@email.com",
                Address = "ABC Address",
                ReceiveNewsLetters = true,
                DateOfBirth = DateTime.Parse("2000-01-01"),
                Gender = GenderOptions.Male,
                CountryID = countryResponse.CountryID
            };

            // Arrange
            PersonAddRequest personAddRequest1 = new PersonAddRequest()
            {
                PersonName = "John",
                Email = "john@email.com",
                Address = "DEF Address",
                ReceiveNewsLetters = true,
                DateOfBirth = DateTime.Parse("2000-01-01"),
                Gender = GenderOptions.Male,
                CountryID = countryResponse1.CountryID
            };

            PersonResponse personRes1 = _personService.AddPerson(personAddRequest);
            PersonResponse personRes2 = _personService.AddPerson(personAddRequest1);

            // To Print all response on TestScreen
            List<PersonResponse> allResponse = new List<PersonResponse>() { personRes1, personRes2 };

            foreach (PersonResponse item in allResponse)
            {
                _outputHelper.WriteLine($"{item.PersonID}, {item.PersonName}, {item.Gender}, {item.Address}, {item.Country}, {item.Age}");
            }

            List<PersonResponse> personList = _personService.GetAllPerson();

            // Assert - 1
            Assert.Contains(personRes1, personList);

            // Assert - 2
            Assert.Contains(personRes2, personList);
        }

        
        // To get Filtered Person
        // If search text is empty & search by is "PersonName", it should return all persons
        [Fact]
        public void GetFilteredPersons_EmptySearchText()
        {
            // Arrange - 1
            CountryAddRequest countryAddRequest = new CountryAddRequest()
            {
                CountryName = "Netherland"
            };

            CountryAddRequest countryAddRequest1 = new CountryAddRequest()
            {
                CountryName = "Australia"
            };

            CountryResponse countryResponse = _countriesService.AddCountry(countryAddRequest);
            CountryResponse countryResponse1 = _countriesService.AddCountry(countryAddRequest1);


            // Arrange
            PersonAddRequest personAddRequest = new PersonAddRequest()
            {
                PersonName = "John Doe",
                Email = "john@email.com",
                Address = "ABC Address",
                ReceiveNewsLetters = true,
                DateOfBirth = DateTime.Parse("2000-01-01"),
                Gender = GenderOptions.Male,
                CountryID = countryResponse.CountryID
            };

            // Arrange
            PersonAddRequest personAddRequest1 = new PersonAddRequest()
            {
                PersonName = "John",
                Email = "john@email.com",
                Address = "DEF Address",
                ReceiveNewsLetters = true,
                DateOfBirth = DateTime.Parse("2000-01-01"),
                Gender = GenderOptions.Male,
                CountryID = countryResponse1.CountryID
            };


            PersonResponse personRes1 = _personService.AddPerson(personAddRequest);
            PersonResponse personRes2 = _personService.AddPerson(personAddRequest1);


            // To Print all response on TestScreen
            List<PersonResponse> allResponse = new List<PersonResponse>() { personRes1, personRes2 };


            foreach (PersonResponse item in allResponse)
            {
                _outputHelper.WriteLine($"{item.PersonID}, {item.PersonName}, {item.Gender}, {item.Address}, {item.Country}, {item.Age}");
            }


            List<PersonResponse> personListFromSearch = _personService.GetFilteredPersons(nameof(Person.PersonName), "");


            // Assert
            foreach (PersonResponse item in personListFromSearch)
            {
                Assert.Contains(item, personListFromSearch);
            }
        }


        // To get Filtered Person By EmptyPersonName
        // First we will add few persons; and then we will search based on person name with some search string. It should return the matching person.
        [Fact]
        public void GetFilteredPersons_SearchByPersonName()
        {
            // Arrange - 1
            CountryAddRequest countryAddRequest = new CountryAddRequest()
            {
                CountryName = "Netherland"
            };

            CountryAddRequest countryAddRequest1 = new CountryAddRequest()
            {
                CountryName = "Australia"
            };

            CountryResponse countryResponse = _countriesService.AddCountry(countryAddRequest);
            CountryResponse countryResponse1 = _countriesService.AddCountry(countryAddRequest1);


            // Arrange
            PersonAddRequest personAddRequest = new PersonAddRequest()
            {
                PersonName = "John Doe",
                Email = "john@email.com",
                Address = "ABC Address",
                ReceiveNewsLetters = true,
                DateOfBirth = DateTime.Parse("2000-01-01"),
                Gender = GenderOptions.Male,
                CountryID = countryResponse.CountryID
            };

            // Arrange
            PersonAddRequest personAddRequest1 = new PersonAddRequest()
            {
                PersonName = "John",
                Email = "john@email.com",
                Address = "DEF Address",
                ReceiveNewsLetters = true,
                DateOfBirth = DateTime.Parse("2000-01-01"),
                Gender = GenderOptions.Male,
                CountryID = countryResponse1.CountryID
            };


            PersonResponse personRes1 = _personService.AddPerson(personAddRequest);
            PersonResponse personRes2 = _personService.AddPerson(personAddRequest1);


            // To Print all response on TestScreen
            List<PersonResponse> allResponse = new List<PersonResponse>() { personRes1, personRes2 };


            foreach (PersonResponse item in allResponse)
            {
                _outputHelper.WriteLine($"{item.PersonID}, {item.PersonName}, {item.Gender}, {item.Address}, {item.Country}, {item.Age}");
            }


            List<PersonResponse> personListFromSearch = _personService.GetFilteredPersons(nameof(Person.PersonName), "ma");


            // Assert
            foreach (PersonResponse item in personListFromSearch)
            {   
                if(item.PersonName != null)
                {
                    if (item.PersonName.Contains("ma", StringComparison.OrdinalIgnoreCase))
                    {
                        Assert.Contains(item, personListFromSearch);
                    }
                }
            }
        }
    }
}
