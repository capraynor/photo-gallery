using Microsoft.EntityFrameworkCore;
using Photo_Gallery.Entities;
using System;
using System.Collections.Generic;

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
    }
}
