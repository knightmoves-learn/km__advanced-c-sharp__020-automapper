using Microsoft.AspNetCore.Mvc;
using HomeEnergyApi.Models;
using HomeEnergyApi.Services;
using AutoMapper;

namespace HomeEnergyApi.Controllers
{
    [ApiController]
    [Route("admin/Homes")]
    public class HomeAdminController : ControllerBase
    {
        private IWriteRepository<int, Home> repository;
        private ZipCodeLocationService zipCodeLocationService;
        private IMapper mapper;

        public HomeAdminController(IWriteRepository<int, Home> repository, ZipCodeLocationService zipCodeLocationService, IMapper mapper)
        {
            this.repository = repository;
            this.zipCodeLocationService = zipCodeLocationService;
            this.mapper = mapper;
        }

        [HttpPost]
        public IActionResult CreateHome([FromBody] HomeDto homeDto)
        {
            Home home = mapper.Map<Home>(homeDto);;
            repository.Save(home);
            return Created($"/Homes/{repository.Count()}", home);
        }

        public IActionResult UpdateHome([FromBody] HomeDto homeDto, [FromRoute] int id)
        {
            Home home = mapper.Map<Home>(homeDto);;
            if (id > (repository.Count() - 1))
            {
                return NotFound();
            }
            repository.Update(id, home);
            return Ok(home);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteHome(int id)
        {
            if (id > repository.Count())
            {
                return NotFound();
            }
            var home = repository.RemoveById(id);
            return Ok(home);
        }

        [HttpPost("Location/{zipCode}")]
        public async Task<IActionResult> ZipLocation([FromRoute] int zipCode)
        {
            Place place = await zipCodeLocationService.Report(zipCode);
            return Ok(place);
        }

        // public Home Map(HomeDto homeDto)
        // {
        //     Home home = new(homeDto.OwnerLastName, homeDto.StreetAddress, homeDto.City);

        //     if (homeDto.MonthlyElectricUsage != null)
        //     {
        //         HomeUsageData homeUsageData = new();
        //         homeUsageData.MonthlyElectricUsage = homeDto.MonthlyElectricUsage;
        //         home.HomeUsageData = homeUsageData;
        //     }
        //     if (homeDto.ProvidedUtilities != null)
        //     {
        //         home.UtilityProviders = [];
        //         foreach (String providedUtilites in homeDto.ProvidedUtilities)
        //         {
        //             UtilityProvider utilityProvider = new();
        //             utilityProvider.ProvidedUtility = providedUtilites;
        //             home.UtilityProviders.Add(utilityProvider);
        //         }
        //     }

        //     return home;
        // }
    }
}