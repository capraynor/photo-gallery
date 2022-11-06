using System.ComponentModel.DataAnnotations.Schema;

namespace Photo_Gallery.Entities
{
    [Table("MediaDirectories")]
    public class MediaDirectory
    {
        public virtual string Path { get; set; }
        public virtual Guid Id { get; set; }
        public virtual ICollection<MediaFile> Photos { get; set; }
        public IEnumerable<string> AllFiles { get
            {
                return Directory.EnumerateFiles(Path, "*.*", SearchOption.AllDirectories);
            } }
    }
}
