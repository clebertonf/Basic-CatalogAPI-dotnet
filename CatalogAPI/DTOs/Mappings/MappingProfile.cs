using AutoMapper;
using CatalogAPI.Models;
namespace CatalogAPI.DTOs.Mappings;
public class MappingProfile : Profile
{
    public MappingProfile() 
    {
        CreateMap<Customer, CustomerDTO>().ReverseMap();
    }
}
