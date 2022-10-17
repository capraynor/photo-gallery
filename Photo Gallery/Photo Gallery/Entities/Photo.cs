using System.ComponentModel.DataAnnotations.Schema;

namespace Photo_Gallery.Entities
{
    [Table("Photos")]
    public class Photo
    {
        public string FilePath { get; set; }
        public string ThumbnailFilePath { get; set; }
        public DateTimeOffset ShottingDate { get; set; }
        public DateTimeOffset CreatedDate { get; set; }

        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string MD5Str { get; set; }
        public Guid Id { get; set; }
    }
}
