using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Photo_Gallery.Entities;
using Photo_Gallery.Services.Abastractions;
using System;

namespace UnitTests
{
    [TestClass]
    public class Photos_Test: PhotoGalleryUnitTest
    {
        [TestMethod]
        public void Photo_Should_Be_Created()
        {
            var photoService = this.ServiceProvider.GetService<IPhotoService>();

            Assert.IsNotNull(photoService);
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
            
        }
    }
}