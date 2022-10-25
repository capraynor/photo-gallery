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
        public void AddMediaDirectory(string path)
        {
            var result = new MediaDirectory()
            {
                Id = Guid.NewGuid(),
                Path = path
            };
            this.context.MediaDirectories.Add(result);
        }

        public IEnumerable<string> GetAllMediaFilePaths(string directoryPath)
        {
            throw new NotImplementedException();
        }

    }
}
