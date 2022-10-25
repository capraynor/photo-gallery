using System.ComponentModel.DataAnnotations.Schema;

namespace Photo_Gallery.Entities
{
    [Table("MediaFiles")]
    public class MediaFile
    {
        public virtual string FilePath { get; set; }
        public virtual string ThumbnailFilePath { get; set; }
        public virtual DateTimeOffset ShottingDate { get; set; }
        public virtual DateTimeOffset CreatedDate { get; set; }

        public virtual double Longitude { get; set; }
        public virtual double Latitude { get; set; }
        public virtual string MD5Str { get; set; }
        public virtual Guid Id { get; set; }
        public virtual Guid MediaDirectoryId { get; set; }
        public virtual MediaDirectory MediaDirectory { get; set; }
    }
}
