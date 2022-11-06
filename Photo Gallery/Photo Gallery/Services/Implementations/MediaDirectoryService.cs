using Photo_Gallery.Entities;
using Photo_Gallery.Infrastructures;
using Photo_Gallery.Services.Abastractions;

namespace Photo_Gallery.Services.Implementations
{
    public class MediaDirectoryService : IMediaDirectoryService
    {
        private PhotoGalleryDBContext context;

        public MediaDirectoryService(PhotoGalleryDBContext context)
        {
            this.context = context;
        }
        public MediaDirectory AddMediaDirectory(string path)
        {
            var result = new MediaDirectory()
            {
                Id = Guid.NewGuid(),
                Path = path
            };
            this.context.MediaDirectories.Add(result);
            this.context.SaveChanges();

            return result;
        }

        public IEnumerable<MediaDirectory> GetAllMediaDirectories()
        {
            return context.MediaDirectories;
        }

        public MediaDirectory? GetMediaDirectoryById(Guid mediaDirectoryId)
        {
            return (from c in context.MediaDirectories where c.Id == mediaDirectoryId select c).FirstOrDefault();
        }
    }
}
