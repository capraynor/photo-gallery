using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Photo_Gallery.Entities;
using Photo_Gallery.Infrastructures;
using Photo_Gallery.Services.Abastractions;

namespace Photo_Gallery.Services.Implementations
{
    public class MediaFileService : IMediaFileService
    {
        PhotoGalleryDBContext Context { get; }

        private ILogger<MediaFileService> _logger;

        public MediaFileService(PhotoGalleryDBContext context, ILogger<MediaFileService> logger)
        {
            this.Context = context;
            this._logger = logger;
        }
        public void AddMediaFile(MediaFile mediaFile)
        {
            this.Context.Add(mediaFile);
            this.Context.SaveChanges();
        }

        public MediaFile AddMediaFileFromPath(string? filePath, Guid directoryId)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                var mediaFile = MediaFile.FromFile(filePath);
                mediaFile.MediaDirectoryId = directoryId;
                var duplicatedMediaFile = (from c in Context.MediaFiles where c.FilePath == mediaFile.FilePath select c).FirstOrDefault();
                if (mediaFile.FileType == MediaFileType.Unknown)
                {
                    _logger.LogWarning($"The file: {filePath} with mime type {mediaFile.MimeType} cannot be recognized as a media file. ");
                    return mediaFile;
                }
                else if (duplicatedMediaFile != null)
                {
                    _logger.LogWarning($"The file: {filePath} already exists.");
                }
                else
                {
                    this.AddMediaFile(mediaFile);
                    return mediaFile;
                }
            }

            return null;
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

        public async Task<int> GetMediaFileTotalCount()
        {
            return await this.Context.MediaFiles.CountAsync();
        }

        public async Task<List<MediaFile>> GetMediaFile(int skip, int take)
        {
            var query = from c in this.Context.MediaFiles orderby c.ShottingDate ascending select c;
            var result = query.Skip(skip).Take(take).ToList();
            return result;
        }
    }
}
