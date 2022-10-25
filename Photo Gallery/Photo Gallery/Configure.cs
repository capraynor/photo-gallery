using Microsoft.EntityFrameworkCore;
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

            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddDbContext<PhotoGalleryDBContext>(
                options =>
                {
                    var sqliteConnectionStr = config.GetConnectionString("DefaultConnection");

                    options.UseSqlite(sqliteConnectionStr);
                }
                );
            services.AddScoped<IMediaFileService, MediaFileService>();
        }
    }
}
