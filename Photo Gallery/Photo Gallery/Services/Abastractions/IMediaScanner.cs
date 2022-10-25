using Photo_Gallery.Entities;
using Photo_Gallery.Enumerations;
using System.Collections.Concurrent;

namespace Photo_Gallery.Services.Abastractions
{
    public interface IMediaScanner: IHostedService
    {
        void AddMediaDirectoryToScanList();
        void RemoveMediaDirectoryToScanList();
        MediaDirectoryScanStatus GetScanningStatus();
    }
}
