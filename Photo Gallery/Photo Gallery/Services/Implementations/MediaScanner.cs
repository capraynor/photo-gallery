using Microsoft.EntityFrameworkCore;
using Photo_Gallery.Entities;
using Photo_Gallery.Enumerations;
using Photo_Gallery.Infrastructures;
using Photo_Gallery.Services.Abastractions;
using System.Collections.Concurrent;

namespace Photo_Gallery.Services.Implementations
{
    public class MediaScanner : BackgroundService, IMediaScanner
    {
        BlockingCollection<MediaDirectory> MediaDirectories { get; set; }
        public IServiceProvider Services { get; }

        public MediaScanner(IServiceProvider services)
        {
            this.MediaDirectories = new BlockingCollection<MediaDirectory>();
            Services = services;
        }
        public void AddMediaDirectoryToScanList()
        {
            throw new NotImplementedException();
        }

        public MediaDirectoryScanStatus GetScanningStatus()
        {
            throw new NotImplementedException();
        }

        public void RemoveMediaDirectoryToScanList()
        {
            throw new NotImplementedException();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return this.StartScanning();
        }

        protected async Task StartScanning()
        {
            using (var scope = Services.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<PhotoGalleryDBContext>();
                var allDirectories = await _context.MediaDirectories.ToListAsync();
                foreach (var directory in allDirectories)
                {
                    this.MediaDirectories.Add(directory);
                }
            }

            while (!this.MediaDirectories.IsAddingCompleted)
            {
            }
        }

    }
}
