using Photo_Gallery.Entities;

namespace Photo_Gallery.Services.Abastractions
{
    public interface IMediaDirectoryService
    {
        void AddMediaDirectory(string photoDirectoryPath);
        IEnumerable<string> GetAllMediaFilePaths(string directoryPath);
    }
}
