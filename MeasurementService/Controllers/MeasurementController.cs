using MeasurementApplication.DTO;
using MeasurementApplication.Interfaces;
using MeasurementService.FeatureToggle;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace MeasurementService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MeasurementController : ControllerBase
    {
        private readonly IMeasurementService _measurementService;
        private readonly ILogger<MeasurementController> _logger;
        private readonly IFeatureToggle _featureToggle;

        public MeasurementController(IMeasurementService measurementService, ILogger<MeasurementController> logger, IFeatureToggle featureToggle)
        {
            _measurementService = measurementService;

            _logger = logger;
            _featureToggle = featureToggle;

        }

        [HttpGet]
        [Route("Rebuild")]
        public IActionResult Rebuild()
        {
            _measurementService.Rebuild();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddMeasurement([FromBody] CreateMeasurementDTO newMeasurement)
        {


            var feature = await _featureToggle.IsFeatureEnabled("AddNewMeasurement");

            if (!feature)
            {
                _logger.LogInformation("Add a new Measurement feature is disabled.");
                return BadRequest("This feature is currently disabled.");
            }

            _logger.LogInformation("Create the measurement with the values " + newMeasurement);
            try

            var tracer = OpenTelemetry.Trace.TracerProvider.Default.GetTracer("MeasurementService-API");
            using (var span = tracer.StartActiveSpan("AddMeasurement"))

            {
                span.SetAttribute("MeasurementDetails", newMeasurement.ToString());
                Log.Logger.Information($"Creating the measurement with the values: {newMeasurement}");
                try
                {
                    await _measurementService.AddMeasurementAsync(newMeasurement);
                    return Ok();
                }
                catch (Exception ex)
                {
                    Log.Logger.Error($"Measurement couldn't be added: {ex.Message}");
                    return StatusCode(500, ex.Message);
                }
            }
        }

        [HttpGet]
        [Route("GetByPatientSSN/{patientSSN}")]
        public async Task<IActionResult> GetMeasurementsByPatientSSN(string patientSSN)
        {
            var tracer = OpenTelemetry.Trace.TracerProvider.Default.GetTracer("MeasurementService-API");
            using (var span = tracer.StartActiveSpan("GetMeasurementByPatientSSN"))
            {
                span.SetAttribute("PatientSSN", patientSSN);


            var feature = await _featureToggle.IsFeatureEnabled("GetMeasurementsByPatientSSN");

            if (!feature)
            {
                _logger.LogInformation("Get Measurements By PatientSSN feature is disabled.");
                return BadRequest("This feature is currently disabled.");
            }


            _logger.LogInformation($"Trying to retrieve the measurements for the patient with the given ssn {patientSSN}");
            try
            {
                var measurements = await _measurementService.GetMeasurementsByPatientSSNAsync(patientSSN);
                if (!measurements.Any())

                if (string.IsNullOrEmpty(patientSSN))

                {
                    return BadRequest("Patient SSN is required.");
                }

                Log.Logger.Information($"Trying to retrieve the measurements for the patient with the given ssn {patientSSN}");
                try
                {
                    var measurements = await _measurementService.GetMeasurementsByPatientSSNAsync(patientSSN);
                    if (!measurements.Any())
                    {
                        return NotFound($"No measurements found for patient with SSN: {patientSSN}");
                    }

                    return Ok(measurements);
                }
                catch (Exception ex)
                {
                    Log.Logger.Error(ex, "Could not retrieve the measurements");
                    return StatusCode(500, ex.Message);
                }
            }
        }

        [HttpDelete]
        [Route("DeleteByPatientSSN/{patientSSN}")]
        public async Task<IActionResult> DeleteMeasurementsByPatientSSN(string patientSSN)
        {
            var tracer = OpenTelemetry.Trace.TracerProvider.Default.GetTracer("MeasurementService-API");
            using (var span = tracer.StartActiveSpan("DeleteMeasurementsByPatientSSN"))
            {
                span.SetAttribute("PatientSSN", patientSSN);


            var feature = await _featureToggle.IsFeatureEnabled("DeleteMeasurementsByPatientSSN");

            if (!feature)
            {
                _logger.LogInformation("Delete Measurements By PatientSSN feature is disabled.");
                return BadRequest("This feature is currently disabled.");
            }

            try
            {
                await _measurementService.DeleteMeasurementsByPatientSSNAsync(patientSSN);
                _logger.LogInformation("Measurements for patient SSN: {PatientSSN} deleted successfully", patientSSN);
                return NoContent(); 
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "An error occurred while deleting measurements for the patient with the SSN: {PatientSSN}", patientSSN);
                return StatusCode(500, ex.Message);

                if (string.IsNullOrEmpty(patientSSN))
                {
                    return BadRequest("Patient SSN is required");
                }

                try
                {
                    await _measurementService.DeleteMeasurementsByPatientSSNAsync(patientSSN);
                    Log.Logger.Information("Measurements for patient SSN: {PatientSSN} deleted successfully", patientSSN);
                    return NoContent();
                }
                catch (Exception ex)
                {
                    Log.Logger.Error(ex, "An error occurred while deleting measurements for the patient with the SSN: {PatientSSN}", patientSSN);
                    return StatusCode(500, ex.Message);
                }

            }
        }

        [HttpDelete]
        [Route("{measurementId}")]
        public async Task<IActionResult> DeleteMeasurement(int measurementId)
        {
            var tracer = OpenTelemetry.Trace.TracerProvider.Default.GetTracer("MeasurementService-API");
            using (var span = tracer.StartActiveSpan("DeleteMeasurement"))
            {
                span.SetAttribute("Measurementid", measurementId);


            var feature = await _featureToggle.IsFeatureEnabled("DeleteMeasurement");

            if (!feature)
            {
                _logger.LogInformation("Delete Measurement feature is disabled.");
                return BadRequest("This feature is currently disabled.");
            }

            try
            {
                await _measurementService.DeleteMeasurementById(measurementId);
                _logger.LogInformation("Measurement with Id: {MeasurementId} was deleted", measurementId);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Error while deleting the measurement: {Message}", ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting measurement with ID: {MeasurementId}", measurementId);
                return StatusCode(500, ex.Message);

                if (measurementId < 1)
                {
                    Log.Logger.Error("id cannot be less than 1");
                    return BadRequest("invalid id provided");
                }

                try
                {
                    await _measurementService.DeleteMeasurementById(measurementId);
                    Log.Logger.Information("Measurement with Id: {MeasurementId} was deleted", measurementId);
                    return NoContent();
                }
                catch (ArgumentException ex)
                {
                    Log.Logger.Error(ex, "Error while deleting the measurement: {Message}", ex.Message);
                    return BadRequest(ex.Message);
                }
                catch (Exception ex)
                {
                    Log.Logger.Error(ex, "Error deleting measurement with ID: {MeasurementId}", measurementId);
                    return StatusCode(500, ex.Message);
                }

            }
        }

        [HttpPut]
        [Route("{measurementId}")]
        public async Task<IActionResult> UpdateMeasurement([FromRoute] int measurementId, [FromBody] UpdateMeasurementDTO updatedMeasurement)
        {

            var feature = await _featureToggle.IsFeatureEnabled("UpdateMeasurement");

            if (!feature)
            {
                _logger.LogInformation("Update Measurement feature is disabled.");
                return BadRequest("This feature is currently disabled.");
            }

            _logger.LogInformation("trying to update the measurement with Id: {MeasurementId}", measurementId);
            try

            var tracer = OpenTelemetry.Trace.TracerProvider.Default.GetTracer("MeasurementService-API");
            using (var span = tracer.StartActiveSpan("UpdateMeasurement"))

            {
                span.SetAttribute("measurementId", measurementId);
                span.SetAttribute("updatedMeasurementDetails", updatedMeasurement.ToString());

                Log.Logger.Information("trying to update the measurement with Id: {MeasurementId}", measurementId);
                try
                {
                    await _measurementService.UpdateMeasurementAsync(measurementId, updatedMeasurement);
                    return Ok();
                }
                catch (Exception ex)
                {
                    Log.Logger.Error(ex, "Error updating the measurement");
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpPut]
        [Route("MarkAsSeen/{measurementId}")]
        public async Task<IActionResult> MarkMeasurementAsSeen([FromRoute] int measurementId)
        {

            var feature = await _featureToggle.IsFeatureEnabled("MarkMeasurementAsSeen");

            if (!feature)
            {
                _logger.LogInformation("Mark Measurement As Seen feature is disabled.");
                return BadRequest("This feature is currently disabled.");
            }

            try
            {
                await _measurementService.MarkMeasurementAsSeenAsync(measurementId);
                _logger.LogInformation("Measurement with Id {MeasurementId} marked as seen", measurementId);
                return Ok("Measurement marked as seen");
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, "Measurement with ID: {MeasurementId} was not found", measurementId);
                return NotFound(ex.Message);
            }
            catch (Exception ex)

            var tracer = OpenTelemetry.Trace.TracerProvider.Default.GetTracer("MeasurementService-API");
            using (var span = tracer.StartActiveSpan("MarkMeasurementAsSeen"))

            {
                span.SetAttribute("measurementId", measurementId);
                Log.Logger.Information("Measurement with Id {MeasurementId} marked as seen", measurementId);
                try
                {
                    await _measurementService.MarkMeasurementAsSeenAsync(measurementId);
                    return Ok("Measurement marked as seen");
                }
                catch (KeyNotFoundException ex)
                {
                    Log.Logger.Error(ex, "Measurement with ID: {MeasurementId} was not found", measurementId);
                    return NotFound(ex.Message);
                }
                catch (Exception ex)
                {
                    Log.Logger.Error(ex, "Failed to mark the measurement with the Id: {MeasurementId} as seen", measurementId);
                    return StatusCode(500, ex.Message);
                }
            }
        }
    }
}
