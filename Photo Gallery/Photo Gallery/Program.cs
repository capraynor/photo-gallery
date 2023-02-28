using Microsoft.EntityFrameworkCore;
using Photo_Gallery;
using Photo_Gallery.Infrastructures;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//builder.Services.AddDbContext<PhotoGalleryDBContext>(
//    options =>
//    {
//        var sqliteConnectionStr = builder.Configuration.GetConnectionString("SqliteConnectionString");

//        options.UseSqlite("name=ConnectionStrings:DefaultConnection");
//    }
//    );

Configure.ConfigureServices(builder.Services, builder.Configuration);

builder.Services.AddSwaggerGen(c =>
{
    c.ExampleFilters();
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,
  $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
    
});
builder.Logging.ClearProviders();
builder.Logging.AddConsole();


builder.Services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    //app.UseSwaggerUI(c =>
    //{
    //    c.DefaultModelRendering(Swashbuckle.AspNetCore.SwaggerUI.ModelRendering.Model);
    //    c.DefaultModelExpandDepth(3);
        
    //});
    app.UseReDoc(
        c=>
        {
            c.RoutePrefix = "swagger";
            c.RequiredPropsFirst();
        });


}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();