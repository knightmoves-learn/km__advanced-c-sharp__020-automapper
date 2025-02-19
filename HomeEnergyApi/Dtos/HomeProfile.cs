using AutoMapper;
using HomeEnergyApi.Migrations;
using HomeEnergyApi.Models;

namespace HomeEnergyApi.Dtos
{
    public class HomeProfile : Profile
    {
        public HomeProfile()
        {
            CreateMap<HomeDto, Home>()
                .ForMember(dest => dest.HomeUsageData,
                    opt => opt.MapFrom(src => src.MonthlyElectricUsage != null
                    ? new HomeUsageData {MonthlyElectricUsage = src.MonthlyElectricUsage}
                    : null))
                .ForMember(dest => dest.UtilityProviders,
                    opt => opt.MapFrom(src => src.ProvidedUtilities != null
                    ? src.ProvidedUtilities.Select(s => new UtilityProvider { ProvidedUtility = s}).ToList()
                    : null));

        }
    }
}
