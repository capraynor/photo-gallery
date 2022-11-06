using Photo_Gallery.Entities;

namespace Photo_Gallery.Services.Abastractions
{
    public interface IMediaDirectoryService
    {
        MediaDirectory AddMediaDirectory(string photoDirectoryPath);
        MediaDirectory? GetMediaDirectoryById(Guid mediaDirectoryId);
        IEnumerable<MediaDirectory> GetAllMediaDirectories();
    }
}
