using FeatureHubSDK;
﻿using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatientApplication;
using PatientApplication.DTO;
using PatientService.FeatureToggle;
using Serilog;

namespace PatientService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        IPatientService _patientService;

        private readonly ILogger<PatientController> _logger;
        private readonly IFeatureToggle _featureToggle;
        
        public PatientController(IPatientService patientService, ILogger<PatientController> logger, IFeatureToggle featureToggle)
        {
            _patientService = patientService;
            _logger = logger;
            _featureToggle = featureToggle;


        }

        [HttpGet]
        [Route("RebuildDB")]
        public IActionResult RebuildDb()
        {
            _patientService.RebuildDb();
            return Ok();
        }

        [HttpPost]
        [Route("AddPatient")]
        public async Task<IActionResult> AddPatient([FromBody] PatientDTO patient)
        {

            var feature = await _featureToggle.IsFeatureEnabled("AddNewPatient");

            if (!feature)
            {
                _logger.LogInformation("Add a new patient feature is disabled.");
                return BadRequest("This feature is currently disabled.");
            }

            _logger.LogInformation($"Create the patient with following values {patient}");
            try
            {
                await _patientService.AddPatient(patient);
                return Ok();
            }
            catch (Exception ex)

            var tracer = OpenTelemetry.Trace.TracerProvider.Default.GetTracer("PatientService-API");
            using (var span = tracer.StartActiveSpan("AddPatient"))

            {
                span.SetAttribute("PatientDetails", patient.ToString());
                Log.Logger.Information($"Create the patient with following values {patient}");

                try
                {
                    await _patientService.AddPatient(patient);
                    return Ok();
                }
                catch (Exception ex)
                {
                    Log.Logger.Error(ex, $"Patient couldn't be added with exception message {ex}");
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPatients()
        {

            var feature = await _featureToggle.IsFeatureEnabled("GetAllPatients");

            if (!feature)
            {
                _logger.LogInformation("Get all patients feature is disabled.");
                return BadRequest("This feature is currently disabled.");
            }

            _logger.LogInformation("Get all patients");

            try

            var tracer = OpenTelemetry.Trace.TracerProvider.Default.GetTracer("PatientService-API");
            using (var span = tracer.StartActiveSpan("GetAllPatients"))

            {
                Log.Logger.Information("Get all patients");

                try
                {
                    var patients = await _patientService.GetAllPatients();
                    return Ok(patients);
                }
                catch (Exception ex)
                {
                    Log.Logger.Error(ex, $"Patients couldn't be fetched with exception message {ex}");
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpPut]
        [Route("UpdatePatient/{ssn}")]
        public async Task<IActionResult> UpdatePatient([FromRoute] string ssn, [FromBody] PatientDTO patient)
        {


            var feature = await _featureToggle.IsFeatureEnabled("UpdatePatient");

            if (!feature)
            {
                _logger.LogInformation("Update patient feature is disabled.");
                return BadRequest("This feature is currently disabled.");
            }

            _logger.LogInformation($"Update the patient with following ssn {ssn}");
            try
            {
                await _patientService.UpdatePatient(ssn, patient);
                return Ok();
            }
            catch (Exception ex)
            
            var tracer = OpenTelemetry.Trace.TracerProvider.Default.GetTracer("PatientService-API");
            using (var span = tracer.StartActiveSpan("UpdatePatient"))


            {
                span.SetAttribute("ssn", ssn);
                Log.Logger.Information($"Update the patient with following ssn {ssn}");
                try
                {
                    await _patientService.UpdatePatient(ssn, patient);
                    return Ok();
                }
                catch (Exception ex)
                {
                    Log.Logger.Error(ex, $"Patient couldn't be updated with exception message {ex}");
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpDelete]
        [Route("DeletePatient")]
        public async Task<IActionResult> DeletePatient(string ssn)
        {

            var feature = await _featureToggle.IsFeatureEnabled("DeletePatient");

            if (!feature)
            {
                _logger.LogInformation("Delete patient feature is disabled.");
                return BadRequest("This feature is currently disabled.");
            }


            _logger.LogInformation($"Delete the patient with following ssn {ssn}");
            try

            var tracer = OpenTelemetry.Trace.TracerProvider.Default.GetTracer("PatientService-API");
            using (var span = tracer.StartActiveSpan("DeletePatient"))

            {
                Log.Logger.Information($"Delete the patient with following ssn {ssn}");
                try
                {
                    await _patientService.DeletePatient(ssn);
                    return Ok();
                }
                catch (Exception ex)
                {
                    _logger.LogTrace(ex, $"Patient couldn't be deleted with exception message {ex}");
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpGet]
        [Route("GetPatient")]
        public async Task<IActionResult> GetPatient(string ssn)
        {


            var feature = await _featureToggle.IsFeatureEnabled("GetPatient");

            if (!feature)
            {
                _logger.LogInformation("Get patient feature is disabled.");
                return BadRequest("This feature is currently disabled.");
            }

            _logger.LogInformation($"Get the patient with following ssn {ssn}");

            var tracer = OpenTelemetry.Trace.TracerProvider.Default.GetTracer("PatientService-API");
            using (var span = tracer.StartActiveSpan("GetPatient"))

            Log.Logger.Information($"Get the patient with following ssn {ssn}");

            try
            {
                var patient = await _patientService.GetPatient(ssn);
                return Ok(patient);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, $"Patient couldn't be fetched with exception message {ex}");
                return BadRequest(ex.Message);
            }
        }
    }
}
