using Castle.Core.Internal;
using ExifLibrary;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.VisualBasic.FileIO;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;

public enum MediaFileType
{
    Image = 0,
    Video = 1,
    Unknown = 2
}
namespace Photo_Gallery.Entities
{
    [Table("MediaFiles")]
    public class MediaFile
    {

        public static MediaFile FromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(filePath);
            }

            var result = new MediaFile();

            result.FilePath = filePath;
            result.Id = Guid.NewGuid();

            if (result.FileType == MediaFileType.Image)
            {
                result.LoadImageMetaFromFile();
            }else if (result.FileType == MediaFileType.Video)
            {
                result.LoadVideoMetaFromFile();
            }

            result.LoadMD5FromFile();
            return result;
        }

        private void LoadVideoMetaFromFile()
        {
            DateTimeOffset createdDate = File.GetCreationTimeUtc(FilePath);
            this.CreatedDate = this.ShottingDate = createdDate;
        }

        private void LoadImageMetaFromFile()
        {

            var file = ImageFile.FromFile(FilePath);
            var latTag = file.Properties.Get<GPSLatitudeLongitude>(ExifTag.GPSLatitude);
            var lngTag = file.Properties.Get<GPSLatitudeLongitude>(ExifTag.GPSLongitude);
            var lngRefTag = file.Properties.Get(ExifTag.GPSLongitudeRef);
            var latRefTag = file.Properties.Get(ExifTag.GPSLatitudeRef);


           
            DateTimeOffset lastWriteTimeUTC = File.GetLastWriteTimeUtc(FilePath);


            this.ShottingDate = lastWriteTimeUTC;
            this.CreatedDate = lastWriteTimeUTC;

            if (latTag != null)
            {
                Latitude = latTag.ToFloat();

            }

            if (lngTag != null)
            {
                Longitude = lngTag.ToFloat();
            }

            if (latRefTag != null && lngRefTag != null)
            {

                var latRef = latRefTag.Value as ExifLibrary.GPSLatitudeRef?;
                var lngRef = lngRefTag.Value as ExifLibrary.GPSLongitudeRef?;

                if (latRef != null && lngRef != null)
                {
                    if (latRef == ExifLibrary.GPSLatitudeRef.South)
                    {
                        Latitude = -Math.Abs(Latitude);
                    }

                    if (lngRef == ExifLibrary.GPSLongitudeRef.West)
                    {
                        Latitude = -Math.Abs(Latitude);
                    }
                }

            }

        }

        public virtual string FilePath { get; set; }
        public virtual string? ThumbnailFilePath { get; set; }
        public virtual DateTimeOffset ShottingDate { get; set; }
        public virtual DateTimeOffset CreatedDate { get; set; }

        public virtual double Longitude { get; set; }
        public virtual double Latitude { get; set; }
        public virtual string MD5Str { get; set; }
        public virtual Guid Id { get; set; }
        public virtual Guid MediaDirectoryId { get; set; }
        public virtual MediaDirectory MediaDirectory { get; set; }
        public virtual string MimeType
        {
            get
            {
                string fileType;

                new FileExtensionContentTypeProvider().TryGetContentType(FilePath, out fileType);
                return fileType;
            }
        }
        public virtual MediaFileType FileType
        {
            get
            {

                if (string.IsNullOrEmpty(MimeType))
                {
                    return MediaFileType.Unknown;
                }
                if (MimeType.StartsWith("image/"))
                {
                    return MediaFileType.Image;
                }
                if (MimeType.StartsWith("video/"))
                {
                    return MediaFileType.Video;
                }

                return MediaFileType.Unknown;
            }
        }

        private void LoadMD5FromFile()
        {
            using (var md5 = MD5.Create())
            {
                using (var fs = File.OpenRead(this.FilePath))
                {
                    this.MD5Str = BitConverter.ToString(md5.ComputeHash(fs)).Replace("-", "").ToLower();
                }
            }
        }
    }
}
