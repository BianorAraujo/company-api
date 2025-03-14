using AutoMapper;
using CompanyApp.Domain.Entities;
using CompanyApp.Api.ViewModels;

namespace CompanyApp.Api.Configuration;

public class AutomapperConfig : Profile
{
    public AutomapperConfig()
    {
        CreateMap<Company, CompanyViewModel>().ReverseMap();
    }
}