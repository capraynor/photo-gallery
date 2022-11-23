using AutoMapper;
using Photo_Gallery.DTOs;
using Photo_Gallery.Entities;
using Photo_Gallery.Infrastructures;
using Photo_Gallery.Services.Abastractions;

namespace Photo_Gallery.Services.Implementations
{
    public class MediaDirectoryService : IMediaDirectoryService
    {
        private PhotoGalleryDBContext context;

        public IMapper Mapper { get; }

        public MediaDirectoryService(PhotoGalleryDBContext context, IMapper mapper)
        {
            this.context = context;
            this.Mapper = mapper;
        }
        public MediaDirectory AddMediaDirectory(string path)
        {
            var directory = new MediaDirectory()
            {
                Id = Guid.NewGuid(),
                Path = path
            };
            this.context.MediaDirectories.Add(directory);
            this.context.SaveChanges();

            return directory;
        }

        public IEnumerable<MediaDirectory> GetAllMediaDirectories()
        {
            var directories = context.MediaDirectories;
            return directories;
        }

        public MediaDirectory? GetMediaDirectoryById(Guid mediaDirectoryId)
        {
            var result = (from c in context.MediaDirectories where c.Id == mediaDirectoryId select c).FirstOrDefault();
            if (result == null)
            {
                return null;
            }
            var directoryDTO = Mapper.Map<MediaDirectory>(result);
            return directoryDTO;
        }

        public IQueryable<MediaFile>? GetmediaFilesByDirectory(Guid mediaDirectoryId)
        {
            var mediaDirectory = this.GetMediaDirectoryById(mediaDirectoryId);
            if (mediaDirectory == null)
            {
                return null;
            }
            else
            {
                var result = context.Entry(mediaDirectory).Collection(d => d.MediaFiles).Query();
                return result;
            }
        }


        public int GetPhotoCount(Guid photoDirectoryId)
        {
            var directory = this.GetMediaDirectoryById(photoDirectoryId);
            if (directory != null)
            {
                var count = context.Entry(directory)
                        .Collection(d => d.MediaFiles)
                        .Query()
                        .Count();
                return count;
            }

            return 0;
        }
    }
}
