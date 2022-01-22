using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmberArchives.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class WeatherForecastController : ControllerBase
	{
		private readonly IWeatherForecastService _service;
		private readonly ILogger<WeatherForecastController> _logger;

		public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastService service)
		{
			_logger = logger;
			_service = service;
		}

		[HttpGet]
		public IEnumerable<WeatherForecast> Get()
		{
			var result = _service.Get();
			return result;
		}

		//[HttpGet]
		//[Route("currentDay")]
		[HttpGet("currentDay/{max}")]
		public IEnumerable<WeatherForecast> Get2([FromQuery]int take, [FromRoute]int max)
		{
			var result = _service.Get();
			return result;
		}

		[HttpPost]
		public ActionResult<string> Hello([FromBody]string name)
		{
			// HttpContext.Response.StatusCode = 401;			
			// return $"Hello {name}";

			// return StatusCode(401, $"Hello {name}");
			return NotFound($"Hello {name}");
		}

		[HttpPost]
		[Route("generate")]
		public ActionResult<IEnumerable<WeatherForecast>> GetSpecific([FromBody]SpecificWeatherRequest request, 
			[FromQuery]int count)
		{	
			if (count > 0 && request.minTemp < request.maxTemp)
			{
				var result = _service.GetSpecific(count, request.minTemp, request.maxTemp);
				return Ok(result);
			}
			else
			{
				return BadRequest();
			}
		}
	}
}