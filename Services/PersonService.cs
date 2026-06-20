using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
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

        public List<PersonResponse> GetSortedPersons(List<PersonResponse> allPersons, string sortBy, SortOrderOptions sortOrder)
        {
            if (string.IsNullOrEmpty(sortBy))
            {
                return allPersons;
            }

            List<PersonResponse> sortedPerson =

            // using SwitchExpression

            // ASC - PersonName
            (sortBy, sortOrder) switch
            {
                // ASC - PersonName
                (nameof(PersonResponse.PersonName), SortOrderOptions.ASC) => allPersons.OrderBy(temp => temp.PersonName, StringComparer.OrdinalIgnoreCase).ToList(),

                // DESC - PersonName
                (nameof(PersonResponse.PersonName), SortOrderOptions.DESC) => allPersons.OrderByDescending(temp => temp.PersonName, StringComparer.OrdinalIgnoreCase).ToList(),



                // ASC - Email
                (nameof(PersonResponse.Email), SortOrderOptions.ASC) => allPersons.OrderBy(temp => temp.Email, StringComparer.OrdinalIgnoreCase).ToList(),

                // DESC - Email
                (nameof(PersonResponse.Email), SortOrderOptions.DESC) => allPersons.OrderByDescending(temp => temp.Email, StringComparer.OrdinalIgnoreCase).ToList(),



                // DOB - ASC
                (nameof(PersonResponse.DateOfBirth), SortOrderOptions.ASC) => allPersons.OrderBy(temp => temp.DateOfBirth).ToList(),

                // DOB - DESC
                (nameof(PersonResponse.DateOfBirth), SortOrderOptions.DESC) => allPersons.OrderByDescending(temp => temp.DateOfBirth).ToList(),



                // AGE - ASC
                (nameof(PersonResponse.Age), SortOrderOptions.ASC) => allPersons.OrderBy(temp => temp.Age).ToList(),

                // AGE - DESC
                (nameof(PersonResponse.Age), SortOrderOptions.DESC) => allPersons.OrderByDescending(temp => temp.Age).ToList(),



                // Gender - ASC
                (nameof(PersonResponse.Gender), SortOrderOptions.ASC) => allPersons.OrderBy(temp => temp.Gender).ToList(),

                // Gender - DESC
                (nameof(PersonResponse.Gender), SortOrderOptions.DESC) => allPersons.OrderByDescending(temp => temp.Gender).ToList(),



                // Country - ASC
                (nameof(PersonResponse.Country), SortOrderOptions.ASC) => allPersons.OrderBy(temp => temp.Country).ToList(),

                // Country - DESC
                (nameof(PersonResponse.Country), SortOrderOptions.DESC) => allPersons.OrderByDescending(temp => temp.Country).ToList(),

                // Default
                _ => allPersons
            };
            return sortedPerson;
        }

        public PersonResponse PersonUpdate(PersonUpdateRequest personUpdateRequest)
        {
            /*

             1. Check if "personUpdateRequest" is not null
             2. Validate all properties of "personUpdateRequest"
             3. Get the matching "Person" object from List<Person> based on PersonID
             4. Check if matching "Person" object is not null
             5. Update all details from "PersonUpdateRequest" object to "Person" object
             6. Convert the person object from "Person" to "PersonResponse" type
             7. Return PersonResponse object with updated details

             */

            // When PersonUpdate request is null
            if(personUpdateRequest == null)
            {
                throw new ArgumentNullException(nameof(personUpdateRequest));
            }

            PersonResponse? personData = _persons.FirstOrDefault(temp => temp.PersonID == personUpdateRequest.PersonID)?.ToPersonResponse();


            // When PersonID is invalid/empty
            if(personUpdateRequest.PersonID != personData?.PersonID || personUpdateRequest.PersonID == Guid.Empty)
            {
                throw new ArgumentException(nameof(personUpdateRequest.PersonID));
            }


            // When PersonName is null
            if (personUpdateRequest.PersonName == null)
            {
                throw new ArgumentException(nameof(personUpdateRequest.PersonName));
            }

            return null;
        }
    }
}