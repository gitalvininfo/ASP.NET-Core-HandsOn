using _08_Razor_View_Assignment.Models;
using Microsoft.AspNetCore.Mvc;

namespace _08_Razor_View_Assignment.Controllers
{
    public class WeatherController : Controller
    {

        List<WeatherReport> weatherReports = new List<WeatherReport>
        {
            new WeatherReport
            {
                CityUniqueCode = "LDN",
                CityName = "London",
                DateAndTime = DateTime.Parse("2030-01-01 8:00"),
                TemperatureFahrenheit = 33
            },
            new WeatherReport
            {
                CityUniqueCode = "NYC",
                CityName = "New York",
                DateAndTime = DateTime.Parse("2030-01-01 3:00"),
                TemperatureFahrenheit = 60
            },
            new WeatherReport
            {
                CityUniqueCode = "PAR",
                CityName = "Paris",
                DateAndTime = DateTime.Parse("2030-01-01 9:00"),
                TemperatureFahrenheit = 82
            }
        };



        public IActionResult Index()
        {
            return View(weatherReports);
        }

        [HttpGet("weather/{cityCode}")]
        public IActionResult City(string cityCode)
        {
            var matchedCity = weatherReports.FirstOrDefault(weather => weather.CityUniqueCode == cityCode);

            if (matchedCity != null)
            {
                return View(matchedCity);
            }

            return View();
        }
    }
}
