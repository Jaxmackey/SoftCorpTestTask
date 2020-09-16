using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CityGeoMetricServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CityController : ControllerBase
    {
        private readonly ILogger<CityController> _logger;
        private IRepositoryWrapper _repoWrapper;

        public CityController(ILogger<CityController> logger, IRepositoryWrapper repoWrapper)
        {
            _logger = logger;
            _repoWrapper = repoWrapper;
        }

        [HttpGet("", Name = "GetAll")]
        public IActionResult Get()
        {
            try
            {
                return Ok(_repoWrapper.City.GetAll());
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("{cityName}")]
        public IActionResult GetDaysByCityName(string cityName)
        {
            try
            {
                var result = _repoWrapper.GetGeoMetricsByCityName(cityName).ToList();
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
