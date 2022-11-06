using AutoMapper;
using Photo_Gallery.DTOs;
using Photo_Gallery.Entities;

namespace Photo_Gallery.Infrastructures
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<MediaDirectory, MediaDirectoryDTO>()
               /* .ForMember(x => x.PhotosCount, opt => opt.MapFrom(x => x.Photos.Count))*/;
            CreateMap<MediaFile, MediaFileDTO>();
        }
    }
}
