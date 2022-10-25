using Photo_Gallery.Entities;
using Photo_Gallery.Infrastructures;
using Photo_Gallery.Services.Abastractions;

namespace Photo_Gallery.Services.Implementations
{
    public class MediaFileService : IMediaFileService
    {
        PhotoGalleryDBContext Context { get; }
        public MediaFileService(PhotoGalleryDBContext context)
        {
            this.Context = context;
        }
        public void AddMediaFile(MediaFile mediaFile)
        {
            this.Context.Add(mediaFile);
            this.Context.SaveChanges();
        }

        public MediaFile GetMediaFileByPath(string path)
        {
            throw new NotImplementedException();
        }

        public MediaFile? GetMediaFileById(Guid id)
        {
            var result =  (from c in this.Context.MediaFiles where c.Id == id select c).FirstOrDefault();
            return result;
        }

        public void AddMediaFromFile(string photoFilePath)
        {
            throw new NotImplementedException();
        }
    }
}
