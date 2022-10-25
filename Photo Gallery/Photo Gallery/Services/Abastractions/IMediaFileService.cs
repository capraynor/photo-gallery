using Photo_Gallery.Entities;

namespace Photo_Gallery.Services.Abastractions
{
    public interface IMediaFileService
    {
        MediaFile? GetMediaFileById(Guid id);
        MediaFile GetMediaFileByPath(string FilePath);
        void AddMediaFile(MediaFile photo);
        void AddMediaFromFile(string photoFilePath);
    }
}
