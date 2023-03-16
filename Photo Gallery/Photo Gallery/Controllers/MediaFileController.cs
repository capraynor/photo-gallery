using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Photo_Gallery.DTOs;
using Photo_Gallery.Services.Abastractions;

namespace Photo_Gallery.Controllers
{
    [Route("api/media-files")]
    [ApiController]
    public class MediaFileController : ControllerBase
    {
        private IMediaFileService mediaFileService;

        public IMapper Mapper { get; }

        public MediaFileController(IMediaFileService mediaFileService, IMapper mapper)
        {
            this.mediaFileService = mediaFileService;
            this.Mapper = mapper;

        }
        [HttpGet("count")]
        public async Task<int> GetMediaFileCount()
        {
            return await mediaFileService.GetMediaFileTotalCount();
        }

        [HttpGet]
        public async Task<IList<MediaFileDTO>> GetMediaFiles(int skip, int take)
        {

            var resultEntities = await mediaFileService.GetMediaFile(skip, take);
            var resultDTOs = Mapper.Map<List<MediaFileDTO>>(resultEntities);

            return resultDTOs;

        }

        [HttpGet("{mediaFileId}/content")]
        public async Task<IActionResult> GetMediaFileContent(Guid mediaFileId)
        {
            var mediaFile = mediaFileService.GetMediaFileById(mediaFileId);
            if (mediaFile == null)
            {
                return NotFound();
            }
            var result = new FileStreamResult(
                    System.IO.File.OpenRead(mediaFile.FilePath),
                    mediaFile.MimeType
                );
            return result;
        }
    }
}
