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
            CreateMap<MediaDirectory, MediaDirectoryDTO>();
            CreateMap<MediaFile, MediaFileDTO>();
        }
    }
}
