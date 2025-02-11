using AutoMapper;
using MaterMan.Entity.Concrete;
using MaterMan.Models; // MaterialViewModel burada tanımlı olmalı

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Malzeme -> MaterialViewModel
        CreateMap<Malzeme, MaterialViewModel>()
            .ForMember(dest => dest.MalzemeGrupAdi, opt => opt.MapFrom(src => src.MalzemeGrup.GrupAdi))
           // .ForMember(dest => dest.MalzemeBirimAdi, opt => opt.MapFrom(src => src.MalzemeBirim.BirimAdi))
            //.ForMember(dest => dest.Fiyat, opt => opt.MapFrom(src => src.Fiyat != null ? src.Fiyat.GuncelFiyat : 0))
            .ForMember(dest => dest.MalzemeGrupId, opt => opt.MapFrom(src => src.MalzemeGrupId))
            .ForMember(dest => dest.MalzemeBirimId, opt => opt.MapFrom(src => src.MalzemeBirimId));

        // MaterialViewModel -> Malzeme
        CreateMap<MaterialViewModel, Malzeme>()
            .ForMember(dest => dest.MalzemeGrup, opt => opt.MapFrom(src => new MalzemeGrup { Id = src.MalzemeGrupId }))
            .ForMember(dest => dest.MalzemeBirim, opt => opt.MapFrom(src => new MalzemeBirim { Id = src.MalzemeBirimId }));
        //.ForMember(dest => dest.Fiyat, opt => opt.MapFrom(src => new Fiyat { GuncelFiyat = src.Fiyat }));


        CreateMap<Stok, Stok>()
            .ForMember(dest => dest.Malzeme, opt => opt.MapFrom(src => src.Malzeme));

        CreateMap<Malzeme, Malzeme>()
            .ForMember(dest => dest.Stoklar, opt => opt.MapFrom(src => src.Stoklar));
    }
}
