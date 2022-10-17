using Photo_Gallery.Entities;

namespace Photo_Gallery.Services.Abastractions
{
    public interface IPhotoService
    {
        Photo? GetPhotoById(Guid id);
        Photo GetPhotoByFilePath(string FilePath);
        void AddPhoto(Photo photo);
    }
}
