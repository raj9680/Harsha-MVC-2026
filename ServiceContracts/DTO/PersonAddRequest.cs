using Entities;
using ServiceContracts.DTO.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    /// <summary>
    /// Acts as a DTO for inserting a person
    /// </summary>
    public class PersonAddRequest
    {
        [Required(ErrorMessage ="Person Name is required")]
        public string? PersonName { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email Required")]
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage ="Gender Required")]
        public GenderOptions? Gender { get; set; }
        public Guid? CountryID { get; set; }
        public string? Address { get; set; }
        public bool ReceiveNewsLetters { get; set; }

        /// <summary>
        /// Convert the PersonAddRequest object to Person Object
        /// </summary>
        /// <returns></returns>
        public Person ToPerson()
        {
            Person person = new Person()
            {
                PersonName = PersonName,
                Email = Email,
                DateOfBirth = DateOfBirth,
                Gender = Gender.ToString(),
                CountryID = CountryID,
                Address = Address,
                ReceiveNewsLetter = ReceiveNewsLetters
            };
            return person;
        }
    }
}
