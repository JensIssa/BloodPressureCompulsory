using MeasurementApplication.DTO;
using MeasurementApplication.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MeasurementService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MeasurementController : ControllerBase
    {
        private readonly IMeasurementService _measurementService;
        private readonly ILogger<MeasurementController> _logger;

        public MeasurementController(IMeasurementService measurementService, ILogger<MeasurementController> logger)
        {
            _measurementService = measurementService;
            _logger = logger;
        }

        [HttpGet]
        [Route("Rebuild")]
        public IActionResult Rebuild()
        {
            _measurementService.Rebuild();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddMeasurement([FromBody] MeasurementDTO newMeasurement)
        {
            _logger.LogInformation("Create the measurement with the values " + newMeasurement);
            try
            {
                await _measurementService.AddMeasurementAsync(newMeasurement);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Measurement couldn't be added");
                return StatusCode(500, ex.Message);
            }
        }
    }
}
