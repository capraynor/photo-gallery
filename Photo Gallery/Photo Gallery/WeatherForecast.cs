namespace Photo_Gallery
{
    public class WeatherForecast
    {
        /// <summary>
        /// Comment for date
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Comment for TemperatureC
        /// </summary>
        public int TemperatureC { get; set; }

        /// <summary>
        /// Comment for TemperatureF
        /// </summary>
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        /// <summary>
        /// Comment for Summary
        /// </summary>
        public string? Summary { get; set; }
    }
}