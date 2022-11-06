using Photo_Gallery.Entities;
using Photo_Gallery.Infrastructures;
using Photo_Gallery.Services.Abastractions;
using System.Collections.Concurrent;

namespace Photo_Gallery.Services.Implementations
{
    public class MediaFileIndexer : BackgroundService, IMediaFileIndexer
    {
        public MediaFileIndexer(IServiceProvider services)
        {
            this.HighPriorityScanningQueue = new BlockingCollection<ScanMediaFileRequest>(500);
            this.LowPriorityScanningQueue = new BlockingCollection<ScanMediaFileRequest>(500);
            this.Services = services;

        }
        public BlockingCollection<ScanMediaFileRequest> HighPriorityScanningQueue { get; set; }
        public BlockingCollection<ScanMediaFileRequest> LowPriorityScanningQueue { get; set; }
        public ScanMediaFileRequest? TakeOneFromQueue()
        {
            BlockingCollection<ScanMediaFileRequest>.TakeFromAny(new BlockingCollection<ScanMediaFileRequest>[]
            { HighPriorityScanningQueue, LowPriorityScanningQueue}, out ScanMediaFileRequest? filePath);
            return filePath;
        }

        protected bool IsAddingCompleted()
        {
            return this.HighPriorityScanningQueue.IsAddingCompleted && this.LowPriorityScanningQueue.IsAddingCompleted;
        }
        public IServiceProvider Services { get; }
        public string? CurrentIndexingFile { get; set; }

        public void AddPathToScanningList(string filePath)
        {
            throw new NotImplementedException();
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await this.StartIndexing();
        }

        private async Task StartIndexing()
        {
            await Task.Run(() =>
            {
                using (var scope = Services.CreateScope())
                {
                    var _context = scope.ServiceProvider.GetRequiredService<PhotoGalleryDBContext>();
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<MediaFileIndexer>>();
                    var mediaFileService = scope.ServiceProvider.GetRequiredService<IMediaFileService>();
                    while (!IsAddingCompleted())
                    {
                        var mediaScanningFileRequest = this.TakeOneFromQueue();
                        if (mediaScanningFileRequest != null)
                        {
                            mediaFileService.AddMediaFileFromPath(mediaScanningFileRequest.MediaFilePath, mediaScanningFileRequest.MediaDirectoryId);

                        }
                    }
                }
            });
        }
    }
}
