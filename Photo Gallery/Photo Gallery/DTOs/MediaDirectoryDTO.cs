using Photo_Gallery.Entities;

namespace Photo_Gallery.DTOs
{
    public class MediaDirectoryDTO
    {
        public virtual string Path { get; set; }
        public virtual Guid Id { get; set; }
        public virtual int PhotosCount { get; set; }
    }
}
