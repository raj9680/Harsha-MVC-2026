using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using Services.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PersonService : IPersonService
    {
        // private field
        private readonly List<Person> _persons;
        private readonly ICountriesService _countries;

        public PersonService()
        {
            _persons = new List<Person>();
            _countries = new CountriesService();
        }

        // Private method to 
        PersonResponse ConvertPersonToPersonResponse(Person person)
        {
            // Convert the Person Object into PersonResponsetype
            PersonResponse personResponse = person.ToPersonResponse();

            // Get CountryName from CountryService using CountryID
            personResponse.Country = _countries.GetCountryByCountryID(person.CountryID)?.CountryName;

            return personResponse;
        }

        public PersonResponse AddPerson(PersonAddRequest personAddRequest)
        {
            /// Steps
            /// 1. Check if "personAddRequest" is not null
            /// 2. Validate all properties of "personAddRequest"
            /// 3. Convert "personAddRequest" from "PersonAddRequest" type to Person
            /// 4. Generate new PersonID
            /// 5. Then add it into List<Person>
            /// 6. Return PersonResponse object with generated PersonID
            
            if(personAddRequest == null)
            {
                throw new ArgumentNullException(nameof(PersonAddRequest));
            }

            if (string.IsNullOrEmpty(personAddRequest.PersonName))
            {
                throw new ArgumentNullException("PersonName can't be blank");
            }

            // 1. Validate all properties
            ValidationHelper.ModelValidation(personAddRequest);


            // 2. Convert PersonAddRequest into Person Type
            Person personObj = personAddRequest.ToPerson();

            // 3. generate personID
            personObj.PersonID = Guid.NewGuid();

            // 4. add Person to Data Store
            _persons.Add(personObj);

            // 5. convert the Person object into PersonResponse type
            return ConvertPersonToPersonResponse(personObj);
        }

        public List<PersonResponse> GetAllPerson()
        {
            List<PersonResponse> allPerson = _persons.Select(p => p.ToPersonResponse()).ToList();
            return allPerson;
        }

        public PersonResponse? GetPersonByID(Guid? PersonID)
        {
            /* 
             1. Check if "personID" is not null.
             2. Get matching person from List<Person> based personId.
             3. Convert matching person object from "Person" to "PersonResponse".
             4. Return PersonResponse object.
            */

            if(PersonID == null)
                return null;

            Person? person = _persons.FirstOrDefault(p=>p.PersonID == PersonID);
            if(person == null)
                return null;

            PersonResponse response = person.ToPersonResponse();
            return response;
        }


        // Very Imp.
        public List<PersonResponse> GetFilteredPersons(string searchBy, string? searchString)
        {
            /*
             1. Check if "searchBy" is not null
             2. Get matching persons from List<Person> based on given searchBy and  
                search string.
             3. Convert the matching persons from "Person" type to "PersonResponse" 
                type.
             4. Return all matching PersonResponse objects.
             */

            List<PersonResponse> allPersons = GetAllPerson();
            List<PersonResponse> matchingPersons = allPersons;

            if(string.IsNullOrEmpty(searchBy) || string.IsNullOrEmpty(searchString))
            {
                return matchingPersons;
            }

            switch (searchBy)
            {
                case nameof(Person.PersonName): 
                    matchingPersons = allPersons.Where(temp => 
                    (!string.IsNullOrEmpty(temp.PersonName) ? temp.PersonName.Contains(
                        searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;


                case nameof(Person.Email):
                    matchingPersons = allPersons.Where(temp =>
                    (!string.IsNullOrEmpty(temp.Email) ? temp.Email.Contains(
                        searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;


                // Note: Below DOB is of DateTime so, Contains methods won't work so needed to convert the same in string first
                case nameof(Person.DateOfBirth):
                    matchingPersons = allPersons.Where(temp => (temp.DateOfBirth != null) ? temp.DateOfBirth.Value.ToString("dd MM yyyy").Contains(
                        searchString, StringComparison.OrdinalIgnoreCase) : true).ToList();
                    break;


                case nameof(Person.Gender):
                    matchingPersons = allPersons.Where(temp => (temp.Gender != null) ? temp.Gender.Contains(
                        searchString, StringComparison.OrdinalIgnoreCase) : true).ToList();
                    break;


                case nameof(Person.CountryID):
                    matchingPersons = allPersons.Where(temp =>
                    (!string.IsNullOrEmpty(temp.Country) ? temp.Country.Contains(
                        searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;


                case nameof(Person.Address):
                    matchingPersons = allPersons.Where(temp =>
                    (!string.IsNullOrEmpty(temp.Address) ? temp.Address.Contains(
                        searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;


                default: matchingPersons = allPersons; break;
            }

            return matchingPersons;
        }
    }
}
