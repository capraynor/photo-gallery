using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Photo_Gallery.DTOs;
using Photo_Gallery.Entities;
using Photo_Gallery.Services.Abastractions;

namespace Photo_Gallery.Controllers
{
    [Route("api/media-directories")]
    [ApiController]
    public class MediaDirectoryController : ControllerBase
    {
        private IMediaDirectoryService MediaDirectoryService { get; }
        public IMediaDirectoryScanner DirectoryScanner { get; }
        public IMapper Mapper { get; }

        public MediaDirectoryController(
            IMediaDirectoryService mediaDirectoryService, 
            IMapper mapper,
            IMediaDirectoryScanner mediaDirectoryScanner) : base()
        {
            this.MediaDirectoryService = mediaDirectoryService;
            this.DirectoryScanner = mediaDirectoryScanner;
            this.Mapper = mapper;
        }


        [HttpPut]
        public MediaDirectoryDTO AddMediaDirectory(AddMediaDirectoryDTO directory)
        {
            var mediaDirectory = this.MediaDirectoryService.AddMediaDirectory(directory.Path);
            var result = this.Mapper.Map<MediaDirectoryDTO>(mediaDirectory);
            return result;
        }

        [HttpGet]
        public IEnumerable<MediaDirectoryDTO> GetAllMediaDirectories()
        {
            var allDirectories = this.MediaDirectoryService.GetAllMediaDirectories();

            var result = this.Mapper.Map<List<MediaDirectoryDTO>>(allDirectories);
            return result;
        }

        [HttpPost("scan-directory")]
        public ActionResult<MediaDirectoryDTO> StartScanDirectory([FromQuery]Guid directoryId)
        {
            var mediaDirectory = this.MediaDirectoryService.GetMediaDirectoryById(directoryId);
            var directoryDTO = Mapper.Map<MediaDirectoryDTO>(mediaDirectory);
            if (mediaDirectory != null)
            {
                this.DirectoryScanner.StartDirectoryIndexing();
                return directoryDTO;
            }
            else
            {
                return NotFound();
            }
        }
    }
}
