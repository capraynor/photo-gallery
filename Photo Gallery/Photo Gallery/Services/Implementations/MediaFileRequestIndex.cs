using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Photo_Gallery.Infrastructures;
using Photo_Gallery.Services.Abastractions;

namespace Photo_Gallery.Services.Implementations
{
    public class MediaFileRequestIndex : IMediaFileRequestIndex
    {
        private PhotoGalleryDBContext context;
        private IMapper mapper;
        private List<MediaFileRequestIndexItem>? Index = new List<MediaFileRequestIndexItem>();

        public MediaFileRequestIndex(PhotoGalleryDBContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
            this.BuildIndex();
        }
        public async Task BuildIndex()
        {
            this.Index = await (from c in this.context.MediaFiles
                          select new MediaFileRequestIndexItem()
                          {
                              MediaDirectoryId = c.MediaDirectoryId,
                              Id = c.Id,
                              MediaFileShootingDate = c.ShottingDate
                          }).ToListAsync();
            return;
        }

        public IList<IMediaFileRequestIndexItem> GetIndexOrderByDate()
        {
            while (this.Index == null)
            {
                Task.Delay(100);
            }
            return (IList<IMediaFileRequestIndexItem>)this.Index.OrderByDescending(x => x.MediaFileShootingDate).ToList();
        }

        public async Task RebuildIndex()
        {
            this.Index = null;
            await this.BuildIndex();
        }

    }

    public class MediaFileRequestIndexItem : IMediaFileRequestIndexItem
    {
        public Guid Id { get; set; }

        public Guid MediaDirectoryId { get; set; }

        public DateTimeOffset MediaFileShootingDate { get; set; }
    }


}
