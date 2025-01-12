using AutoMapper;
using TicketBooking.Entities;
using TicketBooking.Web.ViewModels.KoncertVM;
using TicketBooking.Web.ViewModels.MiejscaVM;

namespace TicketBooking.Web.Controllers.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Koncert, KoncertViewModel>();

            CreateMap<CreateKoncertViewModel, Koncert>().
                ForMember(dest => dest.KoncertImage, opt => opt.Ignore());

            CreateMap<Koncert, EditKoncertViewModel>().
            ForMember(dest => dest.KoncertImageFile, opt => opt.Ignore());
            CreateMap<EditKoncertViewModel, Koncert>().
                 ForMember(dest => dest.KoncertImage, opt => opt.Ignore());
            CreateMap<MiejscaDetail, MiejscaDetailViewModel>()
                .ForMember(dest => dest.NazwaKoncertu, opt => opt.MapFrom(src=>src.Koncert.NazwaKoncertu))
                .ForMember(dest => dest.StatusMiejsca, opt => opt.MapFrom(src=>src.StatusMiejsca.ToString()));
            CreateMap<CreateMiejscaViewModel, MiejscaDetail>();
            CreateMap<MiejscaDetail, EditMiejscaDetailsViewModel>().ReverseMap();
        }


    }
}
