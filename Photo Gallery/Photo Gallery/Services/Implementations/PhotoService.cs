using Photo_Gallery.Entities;
using Photo_Gallery.Infrastructures;
using Photo_Gallery.Services.Abastractions;

namespace Photo_Gallery.Services.Implementations
{
    public class PhotoService : IPhotoService
    {
        PhotoGalleryDBContext Context { get; }
        public PhotoService(PhotoGalleryDBContext context)
        {
            this.Context = context;
        }
        public void AddPhoto(Photo photo)
        {
            this.Context.Add(photo);
            this.Context.SaveChanges();
        }

        public Photo GetPhotoByFilePath(string FilePath)
        {
            throw new NotImplementedException();
        }

        public Photo? GetPhotoById(Guid id)
        {
            var result =  (from c in this.Context.Photos where c.Id == id select c).FirstOrDefault();
            return result;
        }
    }
}
