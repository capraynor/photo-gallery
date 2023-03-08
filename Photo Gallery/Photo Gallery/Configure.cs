using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Photo_Gallery.Infrastructures;
using Photo_Gallery.Services.Abastractions;
using Photo_Gallery.Services.Implementations;

namespace Photo_Gallery
{
    public static class Configure
    {
        public static void ConfigureServices(IServiceCollection services, ConfigurationManager config)
        {

            // Add services to the container.

            services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddDbContext<PhotoGalleryDBContext>(
                options =>
                {
                    var sqliteConnectionStr = config.GetConnectionString("DefaultConnection");

                    options
                        .UseSqlite(sqliteConnectionStr);
                }
                );
            services.AddScoped<IMediaFileService, MediaFileService>();
            services.AddScoped<IMediaDirectoryService, MediaDirectoryService>();
            services.AddSingleton<IMediaFileIndexer, MediaFileIndexer>();
            services.AddSingleton<IMediaDirectoryScanner, MediaDirectoryScanner>();
            services.AddHostedService(p => p.GetRequiredService<IMediaFileIndexer>());
            services.AddHostedService(p => p.GetRequiredService<IMediaDirectoryScanner>());
            services.AddLogging(x =>
            {
                x.AddConsole();
            });


            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

        }
    }
}
