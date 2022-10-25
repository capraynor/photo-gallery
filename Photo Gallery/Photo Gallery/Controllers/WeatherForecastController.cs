using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace Photo_Gallery.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        /// <summary>
        /// test 123456
        /// </summary>
        /// <remarks>
        /// this is a cool API. 
        /// test
        /// test
        /// 
        /// test
        /// <h3>
        /// test
        /// </h3>
        /// <code lang="js">
        /// alert("xss!")</code>
        /// 
        /// 
        /// </remarks>
        /// <param name="f">test</param>
        /// <returns>
        /// the returned value
        /// </returns>
        [HttpPost(Name = "PostWeatherForecast")]
        [SwaggerRequestExample(typeof (WeatherForecast), typeof(WeatherForecastExampleprovider))]
        public IEnumerable<WeatherForecast> Post(WeatherForecast f)
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

    }

    public class WeatherForecastExampleprovider : IExamplesProvider<WeatherForecast>
    {
        public WeatherForecast GetExamples()
        {
            return new WeatherForecast()
            {
                Date = DateTime.Now,
                Summary = "test sumamry",
                TemperatureC = 23
            };
        }
    }
}