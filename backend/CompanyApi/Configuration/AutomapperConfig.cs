using AutoMapper;
using CompanyApi.Business.Models;
using CompanyApi.ViewModels;

namespace CompanyApi.Configuration;

public class AutomapperConfig : Profile
{
    public AutomapperConfig()
    {
        CreateMap<Company, CompanyViewModel>().ReverseMap();
    }
}