using AutoMapper;
using InvestmentOrders.Application.DTOs;
using InvestmentOrders.Domain.Entities;

namespace InvestmentOrders.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CrearOrdenDto, Orden>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.EstadoOrdenId, opt => opt.MapFrom(src => 0)) 
            .ForMember(dest => dest.MontoTotal, opt => opt.Ignore())
            .ForMember(dest => dest.Activo, opt => opt.Ignore())
            .ForMember(dest => dest.EstadoOrden, opt => opt.Ignore())
            .ForMember(dest => dest.Precio, opt => opt.MapFrom(src => src.Precio ?? 0m));

        CreateMap<Orden, OrdenDto>()
            .ForMember(dest => dest.DescripcionEstado, opt => opt.MapFrom(src => src.EstadoOrden.DescripcionEstado));
    }
}