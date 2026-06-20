using Entities;
using ServiceContracts.DTO.Enums;
using ServiceContracts.Enumss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class PersonResponse
    {
        public Guid PersonID { get; set; }
        public string? PersonName { get; set; }
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public Guid? CountryID { get; set; }
        public string? Country { get; set; }
        public string? Address { get; set; }
        public bool ReceiveNewsLetter { get; set; }
        public double? Age { get; set; }


        // To convert person response to PersonUpdateRequest & return the same obj
        public PersonUpdateRequest ToPersonUpdateRequest()
        {
            return new PersonUpdateRequest { PersonID = PersonID, PersonName = PersonName, Email = Email, DateOfBirth = DateOfBirth, Gender = (GenderOptions)Enum.Parse(typeof(GenderOptions), Gender, true), CountryID = CountryID, Address = Address, ReceiveNewsLetters = ReceiveNewsLetter };
        }


        // Override Equals Method to check value, bcz byDefault it check for objs
        public override bool Equals(object? obj)
        {
            if(obj == null) return false;
            if(obj.GetType() != typeof(PersonResponse))
            {
                return false;
            }

            PersonResponse personObj = (PersonResponse)obj;
            
            return PersonID == personObj.PersonID && PersonName == personObj.PersonName && Email == personObj.Email && DateOfBirth == personObj.DateOfBirth && Gender == personObj.Gender && CountryID == personObj.CountryID && Address == personObj.Address && Age == personObj.Age;
        }
    }

    // Extension Method
    public static class PersonExtensions
    {
        /// <summary>
        /// An Extension method convert an object of Person class into PersonResponse class
        /// </summary>
        /// <param name="person">Returns the converted PersonResponse object</param>
        public static PersonResponse ToPersonResponse(this Person person)
        {
            return new PersonResponse()
            {
                PersonID = person.PersonID,
                PersonName = person.PersonName,
                Email = person.Email,
                DateOfBirth = person.DateOfBirth,
                Gender = person.Gender,
                Address = person.Address,
                CountryID = person.CountryID,
                Age = (person.DateOfBirth != null) ? Math.Round((DateTime.Now - person.DateOfBirth.Value).TotalDays / 365.25): null
            };
        }
    }
}
