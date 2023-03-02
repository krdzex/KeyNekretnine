using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects.Advert;
using Shared.DataTransferObjects.Auth;
using Shared.DataTransferObjects.Image;
using Shared.DataTransferObjects.User;

namespace KeyNekretnine;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserForRegistrationDto, User>();
        CreateMap<AddAdvertDto, Advert>();
        CreateMap<Advert, AllInfomrationsAboutAdvertDto>();
        CreateMap<Image, ShowImageDto>();
        CreateMap<User, UserInformationDto>()
            .ForMember(dest => dest.First_Name, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.Last_Name, opt => opt.MapFrom(src => src.LastName));
    }
}