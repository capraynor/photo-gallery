using Microsoft.EntityFrameworkCore;
using Photo_Gallery.Entities;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Proxies;

namespace Photo_Gallery.Infrastructures
{
    public class PhotoGalleryDBContext: DbContext
    {
        public DbSet<MediaFile> MediaFiles { get; set; }
        public DbSet<MediaDirectory> MediaDirectories { get; set; }

        public PhotoGalleryDBContext(DbContextOptions<PhotoGalleryDBContext> options)
        : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MediaFile>()
                .HasOne(p => p.MediaDirectory)
                .WithMany(d => d.Photos)
                .HasForeignKey(m => m.MediaDirectoryId);
        }
    }
}
