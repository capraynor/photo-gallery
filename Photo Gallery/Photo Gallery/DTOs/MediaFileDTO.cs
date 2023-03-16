namespace Photo_Gallery.DTOs
{
    public class MediaFileDTO
    {
        public virtual string FilePath { get; set; }
        public virtual string? ThumbnailFilePath { get; set; }
        public virtual DateTime ShottingDate { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual string RequestPath { get
            {
                return $"/api/media-files/{Id}/content";
            } }

        public virtual double Longitude { get; set; }
        public virtual double Latitude { get; set; }
        public virtual string MD5Str { get; set; }
        public virtual Guid Id { get; set; }
        public virtual Guid MediaDirectoryId { get; set; }
    }
}
