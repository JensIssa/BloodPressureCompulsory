using MeasurementApplication.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MeasurementService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MeasurementController : ControllerBase
    {
        private readonly IMeasurementService _measurementService;

        public MeasurementController(IMeasurementService measurementService)
        {
            _measurementService = measurementService;
        }

        [HttpGet]
        [Route("Rebuild")]
        public IActionResult Rebuild()
        {
            _measurementService.Rebuild();
            return Ok();
        }
    }
}
