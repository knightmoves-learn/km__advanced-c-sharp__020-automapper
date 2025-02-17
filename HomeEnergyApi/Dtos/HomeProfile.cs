using AutoMapper;
using HomeEnergyApi.Models;

public class HomeProfile : Profile
{
    public HomeProfile()
    {
        CreateMap<HomeDto, Home>()
            .ForMember(dest => dest.HomeUsageData,
                       opt => opt.MapFrom(src => src.MonthlyElectricUsage != null
                                                 ? new HomeUsageData { MonthlyElectricUsage = src.MonthlyElectricUsage}
                                                 : null))
            .ForMember(dest => dest.UtilityProviders,
                       opt => opt.MapFrom(src => src.ProvidedUtilities != null
                                                 ? src.ProvidedUtilities.Select(p => new UtilityProvider {ProvidedUtility = p}).ToList()
                                                 : new List<UtilityProvider>()));
    }
}