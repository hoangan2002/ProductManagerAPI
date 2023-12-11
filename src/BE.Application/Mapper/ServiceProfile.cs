using AutoMapper;
using BE.Contract.Abstractions.Shared;
using BE.Contract.Services.Product;
using BE.Domain.Entities;

namespace BE.Application.Mapper;

public class ServiceProfile : Profile
{
    public ServiceProfile()
    {
        CreateMap<Product, Response.ProductResponse>().ReverseMap();
        CreateMap<PagedResult<Product>, PagedResult<Response.ProductResponse>>().ReverseMap();
    }
}
