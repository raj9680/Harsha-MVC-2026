using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rotativa.AspNetCore;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services;

namespace CRUD_Operations.Controllers
{
    [Route("person")]
    // [Route("person")] // it take dynamic controller name
    public class PersonController : Controller
    {
        //private fields
        private readonly IPersonService _personsService;
        private readonly ICountriesService _countriesService;

        //constructor
        public PersonController(IPersonService personsService, ICountriesService countriesService)
        {
            _personsService = personsService;
            _countriesService = countriesService;
        }

        [Route("index")]
        // [Route("[index]")] // takes dynamic action name
        [Route("/")]
        public async Task<IActionResult> Index(string searchBy, string? searchString, string sortBy = nameof(PersonResponse.PersonName), SortOrderOptions sortOrder = SortOrderOptions.ASC)
        {
            //Search
            ViewBag.SearchFields = new Dictionary<string, string>()
      {
        { nameof(PersonResponse.PersonName), "Person Name" },
        { nameof(PersonResponse.Email), "Email" },
        { nameof(PersonResponse.DateOfBirth), "Date of Birth" },
        { nameof(PersonResponse.Gender), "Gender" },
        { nameof(PersonResponse.CountryID), "Country" },
        { nameof(PersonResponse.Address), "Address" }
      };
            List<PersonResponse> persons = await _personsService.GetFilteredPersons(searchBy, searchString);
            ViewBag.CurrentSearchBy = searchBy;
            ViewBag.CurrentSearchString = searchString;

            //Sort
            List<PersonResponse> sortedPersons = _personsService.GetSortedPersons(persons, sortBy, sortOrder);
            ViewBag.CurrentSortBy = sortBy;
            ViewBag.CurrentSortOrder = sortOrder.ToString();

            return View(sortedPersons); //Views/Persons/Index.cshtml
        }


        //Executes when the user clicks on "Create Person" hyperlink (while opening the create view)
        [Route("create")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            List<CountryResponse> countries = await _countriesService.GetAllCountries();

            //ViewBag.Countries = countries;  -- below converted to SelectListItem

            // SelectListItems
            // new SelectListItem() { Text = "Name", Value = "AnyValue" };
            // <option value="1">Harsha</option>

            ViewBag.Countries = countries.Select(temp => new SelectListItem()
            {
                Text = temp.CountryName,
                Value = temp.CountryID.ToString()
            });

            return View();
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(PersonAddRequest personAddRequest)
        {
            if (!ModelState.IsValid)
            {
                List<CountryResponse> countries = await _countriesService.GetAllCountries();
                ViewBag.Countries = countries.Select(temp => new SelectListItem()
                {
                    Text = temp.CountryName,
                    Value = temp.CountryID.ToString()
                });

                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return View();
            }

            //call the service method
            PersonResponse personResponse = await _personsService.AddPerson(personAddRequest);

            //navigate to Index() action method (it makes another get request to "persons/index"
            return RedirectToAction("Index", "Person");
        }


        [Route("[action]/{personID}")]  // Eg: /person/edit/1
        [HttpGet]
        public async Task<IActionResult> Edit(Guid personID)
        {
            PersonResponse personResponse = _personsService.GetPersonByID(personID);
            if(personResponse == null)
            {
                return RedirectToAction("Index");
            }

            PersonUpdateRequest personUpdateRequest = personResponse.ToPersonUpdateRequest();

            List<CountryResponse> countries = await _countriesService.GetAllCountries();
            ViewBag.Countries = countries.Select(temp => new SelectListItem()
            {
                Text = temp.CountryName,
                Value = temp.CountryID.ToString()
            });

            return View(personUpdateRequest);
        }


        [Route("[action]/{personID}")]
        [HttpPost]
        public async Task<IActionResult> Edit(PersonUpdateRequest personUpdateRequest)
        {
            PersonResponse? personResponse = _personsService.GetPersonByID(personUpdateRequest.PersonID);

            if(personResponse == null)
            {
                return RedirectToAction("Index");
            }

            if(!ModelState.IsValid)
            {
                List<CountryResponse> countries = await _countriesService.GetAllCountries();
                ViewBag.Countries = countries.Select(temp => new SelectListItem()
                {
                    Text = temp.CountryName,
                    Value = temp.CountryID.ToString()
                });

                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return View();
            }

            PersonResponse personUpdatedResponse = await _personsService.PersonUpdate(personUpdateRequest);
            return RedirectToAction("Index");
        }


        [HttpGet]
        [Route("[action]/{personID}")]
        public IActionResult Delete(Guid? PersonID)
        {
            if(PersonID != null && PersonID != Guid.Empty)
            {
                PersonResponse personResponse = _personsService.GetPersonByID(PersonID);
                if(personResponse == null)
                {
                    return RedirectToAction("Index");
                }

                return View(personResponse);
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        [Route("[action]/{personID}")]
        public IActionResult Delete(PersonUpdateRequest person)
        {
            if (person?.PersonID != null || person?.PersonID != Guid.Empty)
            {
                PersonResponse personResponse = _personsService.GetPersonByID(person?.PersonID);
                _personsService.DeletePerson(personResponse?.PersonID);
                return RedirectToAction("Index");
            }
            return View();
        }

        [Route("PersonsPDF")]
        public async Task<IActionResult> PersonPDF()
        {
            // Get List of Persons
            List<PersonResponse> personResponses = await _personsService.GetAllPerson();

            // Return 
            return new ViewAsPdf("PersonPDF", personResponses, ViewData)
            {
                PageMargins = new Rotativa.AspNetCore.Options.Margins() { Top = 20, Bottom = 20, Right = 20, Left = 20 },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
            };
        }
    }
}
