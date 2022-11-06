using Microsoft.EntityFrameworkCore;
using Photo_Gallery.Entities;
using Photo_Gallery.Enumerations;
using Photo_Gallery.Infrastructures;
using Photo_Gallery.Services.Abastractions;
using System.Collections.Concurrent;

namespace Photo_Gallery.Services.Implementations
{
    public class MediaDirectoryScanner : BackgroundService, IMediaDirectoryScanner
    {
        
        public MediaDirectoryScanner(IServiceProvider services)
        {
            this.ScanningQueue = new BlockingCollection<MediaDirectory>();
            this.Services = services;
        }
        public IServiceProvider Services { get; }


        public BlockingCollection<MediaDirectory> ScanningQueue { get; set; }
        public MediaDirectory? CurrentScanningDirectory { get; set; }

        public MediaDirectoryScanStatus GetScanningStatus()
        {
            throw new NotImplementedException();
        }

        public void StartDirectoryIndexing()
        {
            using (var scope = Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<PhotoGalleryDBContext>();

                var allDirectories = context.MediaDirectories.ToList();
                foreach (var d in allDirectories)
                {
                    AddDirectoryToScanningQueue(d);
                }
            }
        }

        public void AddDirectoryToScanningQueue(MediaDirectory d)
        {
            this.ScanningQueue.Add(d);

        }


        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await this.StartScanning();
        }

        protected async Task StartScanning()
        {
            await Task.Run(() =>
            {
                using (var scope = Services.CreateScope())
                {
                    var mediaFileIndexer = scope.ServiceProvider.GetRequiredService<IMediaFileIndexer>();
                    while (!this.ScanningQueue.IsAddingCompleted)
                    {
                        var directory = this.ScanningQueue.Take();
                        if (directory != null)
                        {
                            CurrentScanningDirectory = directory;
                            foreach (var filePath in directory.AllFiles)
                            {
                                var scanMediaFileRequest = new ScanMediaFileRequest();
                                scanMediaFileRequest.MediaFilePath = filePath;
                                scanMediaFileRequest.MediaDirectoryId = CurrentScanningDirectory.Id;
                                mediaFileIndexer.LowPriorityScanningQueue.Add(scanMediaFileRequest);
                            }
                        }

                        CurrentScanningDirectory = null;
                    }

                }
            });


        }


    }
}
