using Photo_Gallery.DTOs;
using Photo_Gallery.Entities;

namespace Photo_Gallery.Services.Abastractions
{
    public interface IMediaDirectoryService
    {
        MediaDirectory AddMediaDirectory(string photoDirectoryPath);
        MediaDirectory? GetMediaDirectoryById(Guid mediaDirectoryId);
        IQueryable<MediaFile>? GetmediaFilesByDirectory(Guid mediaDirectoryId);
        IEnumerable<MediaDirectory> GetAllMediaDirectories();
        int GetPhotoCount(Guid photoDirectoryId);
    }
}
