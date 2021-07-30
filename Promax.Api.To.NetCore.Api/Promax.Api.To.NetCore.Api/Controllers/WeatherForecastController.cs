using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Promax.Api.To.NetCore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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

        [HttpGet]
        public IEnumerable<TesteDto> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new TesteDto
            {
                Teste = DateTime.Now.AddDays(index).ToShortDateString(),
                Valor = rng.Next(-20, 55),
                Valores = Summaries
            });
        }
    }

    public class TesteDto
    {
        public string Teste { get; set; }
        public int Valor { get; set; }
        public string[] Valores { get; set; }
    }
}
