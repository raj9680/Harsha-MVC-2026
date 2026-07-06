using ServiceContracts.DTO;
using ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface IPersonService
    {
        Task<PersonResponse> AddPerson(PersonAddRequest personAddRequest);

        Task<List<PersonResponse>> GetAllPerson();

        PersonResponse GetPersonByID(Guid? PersonID);

        Task<List<PersonResponse>> GetFilteredPersons(string searchBy, string? searchString);

        /// <summary>
        /// Returns sorted list of persons
        /// </summary>
        /// <param name="allPersons">Returns list of persons to sort</param>
        /// <param name="sortBy">Name of property (key), based on which the person shouldbe sorted</param>
        /// <param name="sortOrder">ASC or DESC</param>
        /// <returns>Returns list of persons after sorting as PersonResponse</returns>
        List<PersonResponse> GetSortedPersons(List<PersonResponse> allPersons, string sortBy, SortOrderOptions sortOrder);

        Task<PersonResponse> PersonUpdate(PersonUpdateRequest personUpdateRequest);

        /// <summary>
        /// Deletes person based on the given person ID
        /// </summary>
        /// <param name="PersonID">Person ID is of Guid Type</param>
        public Task<bool> DeletePerson(Guid? PersonID);
    }
}
