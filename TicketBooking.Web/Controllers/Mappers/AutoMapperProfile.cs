using AutoMapper;
using TicketBooking.Entities;
using TicketBooking.Web.ViewModels.KoncertVM;

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
        }


    }
}
