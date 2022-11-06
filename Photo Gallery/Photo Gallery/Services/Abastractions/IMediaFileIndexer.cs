using Photo_Gallery.Entities;
using System.Collections.Concurrent;

namespace Photo_Gallery.Services.Abastractions
{
    public interface IMediaFileIndexer: IHostedService
    {
        BlockingCollection<ScanMediaFileRequest> HighPriorityScanningQueue { get; set; }
        BlockingCollection<ScanMediaFileRequest> LowPriorityScanningQueue { get; set; }
        string? CurrentIndexingFile { get; set; }
        void AddPathToScanningList(string filePath);

    }

    public class ScanMediaFileRequest
    {
        public Guid MediaDirectoryId { get; set; }
        public string MediaFilePath { get; set; }
    }
}
