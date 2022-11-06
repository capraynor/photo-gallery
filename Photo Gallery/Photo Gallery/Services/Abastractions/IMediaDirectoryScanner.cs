using Photo_Gallery.Entities;
using Photo_Gallery.Enumerations;
using System.Collections.Concurrent;

namespace Photo_Gallery.Services.Abastractions
{
    public interface IMediaDirectoryScanner: IHostedService
    {
        MediaDirectory? CurrentScanningDirectory { get; set; }
        BlockingCollection<MediaDirectory> ScanningQueue { get; set; }
        void StartDirectoryIndexing();
        void AddDirectoryToScanningQueue(MediaDirectory mediaDirectory);

        MediaDirectoryScanStatus GetScanningStatus();
    }
}
