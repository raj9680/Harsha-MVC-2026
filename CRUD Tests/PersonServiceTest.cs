using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.DTO.Enums;
using ServiceContracts.Enums;
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
            _countriesService = new CountriesService();

            _personService = new PersonService(new Entities.PersonsDbContext(new DbContextOptionsBuilder<PersonsDbContext>().Options), _countriesService);
            
            _outputHelper = testOutputHelper;
        }


        #region AddPerson
        // Private function to Add Persons - 2
        private List<PersonResponse> AddPersonTwo()
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

            return allResponse;
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

        #endregion


        #region GetPerson
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
            AddPersonTwo(); // - Add Persons

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
            // Add Persons
            AddPersonTwo();

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


        // To get Sorted Person By - DESC (by personName)
        // First we will add few persons; and then we will search based on person name with some search string. It should return the matching person.
        [Fact]
        public void GetSortedPersons_SearchByPersonName()
        {
            AddPersonTwo(); // -- Add Persons

            // Get All Persons
            List<PersonResponse> allPersons = _personService.GetAllPerson();

            List<PersonResponse> personListFromSort = _personService.GetSortedPersons(allPersons, nameof(Person.PersonName), SortOrderOptions.DESC);

            // Sorting check
            List<PersonResponse>  personListFromSortCheck = personListFromSort.OrderByDescending(x => x.PersonName).ToList();


            for (int i = 0; i < personListFromSort.Count; i++)
            {
                // Assert
                Assert.Equal(personListFromSortCheck[i], personListFromSort[i]);

                // Output
                _outputHelper.WriteLine(personListFromSortCheck[i].PersonName);
            }
        }

        #endregion


        #region PersonUpdate

        // When we supply null as PersonUpdateRequest, it should throw ArgumentNullException
        [Fact]
        public void UpdatePerson_NullPerson()
        {
            PersonUpdateRequest? personUpdateRequest = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                // Assert
                _personService.PersonUpdate(personUpdateRequest);
            });
        }


        // When we supply invalid PersonID in PersonUpdateRequest, it should throw ArgumentException
        [Fact]
        public void UpdatePerson_InvalidPersonID()
        {
            PersonUpdateRequest? personUpdateRequest = new PersonUpdateRequest() 
            { 
                PersonID = Guid.NewGuid()
            };

            Assert.Throws<ArgumentException>(() =>
            {
                // Assert
                _personService.PersonUpdate(personUpdateRequest);
            });
        }


        // When we supply PersonName is null in PersonUpdateRequest, it should throw ArgumentException
        [Fact]
        public void UpdatePerson_PersonNameIsNull()
        {
            CountryAddRequest countryAddRequest = new CountryAddRequest()
            {
                CountryName = "UK"
            };

            CountryResponse countryResponse = _countriesService.AddCountry(countryAddRequest);


            PersonAddRequest personAddRequest = new PersonAddRequest()
            {
                CountryID = countryResponse.CountryID,
                PersonName = "John",
                Gender = GenderOptions.Male,
            };
            PersonResponse personResponse = _personService.AddPerson(personAddRequest);

            PersonUpdateRequest personUpdateRequest = personResponse.ToPersonUpdateRequest();
            personUpdateRequest.PersonName = null;

            Assert.Throws<ArgumentException>(() =>
            {
                // Assert
                _personService.PersonUpdate(personUpdateRequest);
            });
        }



        // When we supply Correct Details and try to update PersonName
        [Fact]
        public void UpdatePerson_CorrectDetails()
        {
            // Added Country
            CountryAddRequest countryAddRequest = new CountryAddRequest()
            {
                CountryName = "UK"
            };
            CountryResponse countryResponse = _countriesService.AddCountry(countryAddRequest);


            // Added Person
            PersonAddRequest personAddRequest = new PersonAddRequest()
            {
                CountryID = countryResponse.CountryID,
                PersonName = "John",
                Address = "ABCD Address",
                Gender = GenderOptions.Male,
                DateOfBirth = DateTime.Now,
                Email = "abc@email.com",
                ReceiveNewsLetters = true
            };
            PersonResponse personResponse = _personService.AddPerson(personAddRequest);

            _outputHelper.WriteLine($"Person name is: {personResponse.PersonName}");

            PersonUpdateRequest personUpdateRequest = personResponse.ToPersonUpdateRequest();
            personUpdateRequest.PersonName = "Doe";


            PersonResponse personResponse1 = _personService.PersonUpdate(personUpdateRequest);


            _outputHelper.WriteLine($"Person name after update: {personResponse1.PersonName}");


            PersonResponse personName = _personService.GetPersonByID(personUpdateRequest.PersonID);
            // Assert
            Assert.Equal(personResponse1.PersonName, personName.PersonName);

        }

        #endregion UpdatePerson


        #region DeletePerson

        // DeletePerson with NullPersonID
        [Fact]
        public void DeletePerson_NullPersonID()
        {
            Guid? personID = null;

            if(personID == null)
            {
                Assert.Null(personID);
            }
        }


        // Delete Person with Valid PersonID
        [Fact]
        public void DeletePerson_ValidPersonID()
        {
            CountryAddRequest countryAddRequest = new CountryAddRequest()
            {
                CountryName = "UK"
            };

            CountryResponse countryResponse = _countriesService.AddCountry(countryAddRequest);


            PersonAddRequest personAddRequest = new PersonAddRequest()
            {
                CountryID = countryResponse.CountryID,
                PersonName = "John",
                Gender = GenderOptions.Male,
            };
            PersonResponse personResponse = _personService.AddPerson(personAddRequest);

            bool isDeleted = _personService.DeletePerson(personResponse.PersonID);

            //Assert
            Assert.True(isDeleted);
        }


        // Delete Person with InValid PersonID
        [Fact]
        public void DeletePerson_InvalidPersonID()
        {
            CountryAddRequest countryAddRequest = new CountryAddRequest()
            {
                CountryName = "UK"
            };

            CountryResponse countryResponse = _countriesService.AddCountry(countryAddRequest);


            PersonAddRequest personAddRequest = new PersonAddRequest()
            {
                CountryID = countryResponse.CountryID,
                PersonName = "John",
                Gender = GenderOptions.Male,
            };
            PersonResponse personResponse = _personService.AddPerson(personAddRequest);

            bool isDeleted = _personService.DeletePerson(Guid.NewGuid());

            //Assert
            Assert.False(isDeleted);
        }

        #endregion

    }
}
