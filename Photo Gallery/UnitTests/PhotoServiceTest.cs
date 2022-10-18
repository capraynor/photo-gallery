using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Photo_Gallery.Entities;
using Photo_Gallery.Infrastructures;
using Photo_Gallery.Services.Abastractions;
using System;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class Photos_Test: PhotoGalleryUnitTest
    {
        [TestInitialize]
        public void CleanupPhotosTable()
        {
            var context = this.Services.GetRequiredService<PhotoGalleryDBContext>();

            context.RemoveRange(context.Photos);
            context.SaveChanges();
        }

        [TestMethod]
        public void Photo_Should_Be_Created()
        {
            var photoService = this.Services.GetRequiredService<IPhotoService>();
            var context = this.Services.GetRequiredService<PhotoGalleryDBContext>();

            var id = Guid.NewGuid();
            var photo = new Photo
            {
                Id = id,
                CreatedDate = DateTime.Now,
                FilePath = "C://test.jpeg",
                Latitude = 8435.32487548734,
                Longitude = 89234.54365465,
                MD5Str = "abcdefghi",
                ShottingDate = DateTime.Now,
                 ThumbnailFilePath = "C://thumbnail.jpg"

            };
            photoService.AddPhoto(photo);

            var result = photoService.GetPhotoById(id);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Id, id);
            Assert.AreEqual(context.Photos.Count(), 1);
        }
    }
}