using Entities;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services.Helpers;

namespace Services
{
    public class PersonService : IPersonService
    {
        // private field
        private readonly PersonsDbContext _dbContext;
        private readonly List<Person> _persons;
        private readonly ICountriesService _countries;

        public PersonService(PersonsDbContext dbContext, ICountriesService countries)
        {
            _dbContext = dbContext;
            _countries = countries;
        }

        // Private method to 
        async Task<PersonResponse> ConvertPersonToPersonResponse(Person person)
        {
            // Convert the Person Object into PersonResponsetype
            PersonResponse personResponse = person.ToPersonResponse();

            // Get CountryName from CountryService using CountryID
            //personResponse.Country = await _countries.GetCountryByCountryID(person.CountryID).CountryName;

            CountryResponse? country = await _countries.GetCountryByCountryID(person.CountryID);

            personResponse.Country = country?.CountryName;

            return personResponse;
        }

        public async Task<PersonResponse> AddPerson(PersonAddRequest personAddRequest)
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
            //_dbContext.Persons.Add(personObj);
            //_dbContext.SaveChanges();

            // Using SP
            _dbContext.sp_InsertPerson(personObj);

            // 5. convert the Person object into PersonResponse type
            return await ConvertPersonToPersonResponse(personObj);
        }

        public async Task<List<PersonResponse>> GetAllPerson()
        
        {
            /* 
               Very Important to understand,  -- It is not allowed to call own instance method 
               inside linq operation as it is refering to current object -- generate the exception 
               and learn what it means

               List<PersonResponse> allPerson = _dbContext.Persons.Select(p => ConvertPersonToPersonResponse(p)).ToList(); 

               Valid - Normal Way to Retreive from DB
               var persons = _dbContext.Persons.ToList();

               var responses = persons
                .Select(ConvertPersonToPersonResponse)
                .ToList();

                return responses;
            */

            var allP = await _dbContext.Persons.Include("Country").ToListAsync(); // "Country" here is not a model class name, but a navigation property from Person Class

            // Using - StoredProcedure to retreive from DB
            //_dbContext.sp_GetAllPersons().Select(temp => ConvertPersonToPersonResponse(temp)).ToList();

            // bcz of async method conversion
            var persons = _dbContext.sp_GetAllPersons();
            //var personResponses = await Task.WhenAll(
            //persons.Select(ConvertPersonToPersonResponse)
            //);
            //return personResponses.ToList();

            List<PersonResponse> personResponses = new();

            foreach (var person in persons)
            {
                personResponses.Add(await ConvertPersonToPersonResponse(person));
            }

            return personResponses;
        }

        public PersonResponse GetPersonByID(Guid? PersonID)
        {
            /* 
             1. Check if "personID" is not null.
             2. Get matching person from List<Person> based personId.
             3. Convert matching person object from "Person" to "PersonResponse".
             4. Return PersonResponse object.
            */

            if(PersonID == null)
                return null;

            Person? person = _dbContext.Persons.FirstOrDefault(p=>p.PersonID == PersonID);
            if(person == null)
                return null;

            //PersonResponse response = person.ToPersonResponse();
            //return response;

            //PersonResponse response = await ConvertPersonToPersonResponse(person);

            // blocking async
            PersonResponse response = ConvertPersonToPersonResponse(person).GetAwaiter().GetResult();
            return response;
        }

        // Very Imp.
        public async Task<List<PersonResponse>> GetFilteredPersons(string searchBy, string? searchString)
        {
            /*
             1. Check if "searchBy" is not null
             2. Get matching persons from List<Person> based on given searchBy and  
                search string.
             3. Convert the matching persons from "Person" type to "PersonResponse" 
                type.
             4. Return all matching PersonResponse objects.
             */

            List<PersonResponse> allPersons = await GetAllPerson();
            List<PersonResponse> matchingPersons = allPersons;

            if(string.IsNullOrEmpty(searchBy) || string.IsNullOrEmpty(searchString))
            {
                return matchingPersons;
            }

            switch (searchBy)
            {
                case nameof(PersonResponse.PersonName): 
                    matchingPersons = allPersons.Where(temp => 
                    (!string.IsNullOrEmpty(temp.PersonName) ? temp.PersonName.Contains(
                        searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;


                case nameof(PersonResponse.Email):
                    matchingPersons = allPersons.Where(temp =>
                    (!string.IsNullOrEmpty(temp.Email) ? temp.Email.Contains(
                        searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;


                // Note: Below DOB is of DateTime so, Contains methods won't work so needed to convert the same in string first
                case nameof(PersonResponse.DateOfBirth):
                    matchingPersons = allPersons.Where(temp => (temp.DateOfBirth != null) ? temp.DateOfBirth.Value.ToString("dd MM yyyy").Contains(
                        searchString, StringComparison.OrdinalIgnoreCase) : true).ToList();
                    break;


                case nameof(PersonResponse.Gender):
                    matchingPersons = allPersons.Where(temp => (temp.Gender != null) ? temp.Gender.Contains(
                        searchString, StringComparison.OrdinalIgnoreCase) : true).ToList();
                    break;


                case nameof(PersonResponse.CountryID):
                    matchingPersons = allPersons.Where(temp =>
                    (!string.IsNullOrEmpty(temp.Country) ? temp.Country.Contains(
                        searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;


                case nameof(PersonResponse.Address):
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

        public async Task<PersonResponse> PersonUpdate(PersonUpdateRequest personUpdateRequest)
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
                throw new ArgumentNullException(nameof(Person));
            }

            // validation
            ValidationHelper.ModelValidation(personUpdateRequest);

            Person? matchingPerson = await _dbContext.Persons.FirstOrDefaultAsync(temp => temp.PersonID == personUpdateRequest.PersonID);

            if(matchingPerson == null)
            {
                throw new ArgumentException("Given person ID doesn't exist");
            }

            // Update all details
            matchingPerson.PersonName = personUpdateRequest.PersonName;
            matchingPerson.Email = personUpdateRequest.Email;
            matchingPerson.CountryID = personUpdateRequest.CountryID;
            matchingPerson.ReceiveNewsLetter = personUpdateRequest.ReceiveNewsLetters;

            await _dbContext.SaveChangesAsync(); // Updates in DB 

            // return matchingPerson.ToPersonResponse();

            return await ConvertPersonToPersonResponse(matchingPerson);
        }

        public async Task<bool> DeletePerson(Guid? PersonID)
        {
            if(PersonID == null)
            {
                throw new ArgumentNullException(nameof(PersonID));
            }

            var person = await _dbContext.Persons.FirstOrDefaultAsync(temp => temp.PersonID == PersonID);
            if (person != null)
            {
                _dbContext.Persons.Remove(person);
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

    }
}