using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Photo_Gallery;
using System;

namespace UnitTests
{
    public abstract class PhotoGalleryUnitTest
    {
        protected IServiceCollection ServiceCollection { get; }
        protected IServiceProvider ServiceProvider { get; }
        protected ConfigurationManager Config { get; }
        public PhotoGalleryUnitTest()
        {
            this.ServiceCollection = new ServiceCollection();
            this.Config = new ConfigurationManager();
            Config.AddJsonFile("./appsettings.json");
            Configure.ConfigureServices(ServiceCollection, Config);

            this.ServiceProvider = this.ServiceCollection.BuildServiceProvider();
        }
    }
}