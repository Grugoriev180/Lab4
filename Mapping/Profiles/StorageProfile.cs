using AutoMapper;
using Lab4.DTO;
using Lab4.Models;

namespace Lab4.Mapping.Profiles;

public class StorageProfile : Profile
{
    public StorageProfile() 
    {
        CreateMap<Products, ProductsDTO>();
        CreateMap<ProductsDTO, Products>();
    }
}