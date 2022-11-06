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

            context.RemoveRange(context.MediaFiles);
            context.RemoveRange(context.MediaDirectories);
            context.SaveChanges();
        }

        [TestMethod]
        public void Media_File_And_Directory_Should_Be_Created()
        {
            var mediaFileService = this.Services.GetRequiredService<IMediaFileService>();
            var mediDirectoryService = this.Services.GetRequiredService<IMediaDirectoryService>();
            var context = this.Services.GetRequiredService<PhotoGalleryDBContext>();


            var mediaDirectory = mediDirectoryService.AddMediaDirectory("./TestData/");

            var id = Guid.NewGuid();
            var mediaFile = new MediaFile
            {
                Id = id,
                CreatedDate = DateTime.Now,
                FilePath = "C://test.jpeg",
                Latitude = 8435.32487548734,
                Longitude = 89234.54365465,
                MD5Str = "abcdefghi",
                ShottingDate = DateTime.Now,
                 ThumbnailFilePath = "C://thumbnail.jpg",
                 MediaDirectoryId = mediaDirectory.Id

            };
            mediaFileService.AddMediaFile(mediaFile);

            var mediaFileResult = mediaFileService.GetMediaFileById(id);
            var mediaDirectoryResult = mediDirectoryService.GetMediaDirectoryById(mediaDirectory.Id);

            Assert.IsNotNull(mediaFileResult);
            Assert.AreEqual(mediaFileResult.Id, id);
            Assert.AreEqual(context.MediaFiles.Count(), 1);

            Assert.AreEqual(context.MediaDirectories.Count(), 1);
            Assert.AreEqual(mediaDirectoryResult.Id, mediaDirectory.Id);
        }

        [TestMethod]
        public void Media_File_Meta_Should_Be_Populated()
        {
            var mediaFile1 = MediaFile.FromFile("./TestData/Photos/DSC01409.JPG");

            Assert.IsNotNull(mediaFile1);
            Assert.IsFalse(string.IsNullOrWhiteSpace(mediaFile1.MD5Str));

            var mediaFile2 = MediaFile.FromFile("./TestData/Photos/IMG_1437.JPG");
            Assert.IsNotNull(mediaFile2);
            Assert.IsFalse(string.IsNullOrWhiteSpace(mediaFile2.MD5Str));


            var mediaFile3 = MediaFile.FromFile("./TestData/Photos/IMG_1077.JPG");
            Assert.IsNotNull(mediaFile3);
            Assert.IsFalse(string.IsNullOrWhiteSpace(mediaFile3.MD5Str));
            Assert.AreNotEqual(mediaFile3.Latitude ,0);
            Assert.AreNotEqual(mediaFile3.Longitude , 0);

        }
    }
}