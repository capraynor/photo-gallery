using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Photo_Gallery;
using System;

namespace UnitTests
{
    public abstract class PhotoGalleryUnitTest
    {
        private IServiceCollection ServiceCollection { get; }
        private IServiceProvider ServiceProvider { get; }
        private ConfigurationManager Config { get; }
        private IServiceScope? ServiceScope { get; set; }
        public IServiceProvider Services { get
            {
                Assert.IsNotNull(this.ServiceScope);
                return this.ServiceScope.ServiceProvider;
            } }

        public PhotoGalleryUnitTest()
        {
            this.ServiceCollection = new ServiceCollection();
            this.Config = new ConfigurationManager();
            Config.AddJsonFile("./appsettings.json");
            Configure.ConfigureServices(ServiceCollection, Config);

            this.ServiceProvider = this.ServiceCollection.BuildServiceProvider();
        }

        [TestInitialize]
        public void CreateServiceScope()
        {
            this.ServiceScope = ServiceProvider.CreateScope();
        }
        [TestCleanup]
        public void DisposeServiceScope()
        {
            this.ServiceScope?.Dispose();
            this.ServiceScope = null;
        }
    }
}