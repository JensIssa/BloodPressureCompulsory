using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatientApplication;
using PatientApplication.DTO;

namespace PatientService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        IPatientService _patientService;

        private readonly ILogger<PatientController> _logger;
        public PatientController(IPatientService patientService, ILogger<PatientController> logger)
        {
            _patientService = patientService;
            _logger = logger;
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
        public async Task<IActionResult>AddPatient([FromBody] PatientDTO patient) 
        {
            _logger.LogInformation($"Create the patient with following values {patient}");
            try
            {
                await _patientService.AddPatient(patient);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogTrace(ex, $"Patient couldn't be added with exception message {ex}");
                return BadRequest(ex.Message);
            }
        }
        public async Task<IActionResult> GetAllPatients()
        {
            _logger.LogInformation("Get all patients");

            try
            {
                var patients = await _patientService.GetAllPatients();
                return Ok(patients);
            }
            catch (Exception ex)
            {
                _logger.LogTrace(ex, $"Patients couldn't be fetched with exception message {ex}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdatePatient")]
        public async Task<IActionResult> UpdatePatient(int ssn)
        {
            _logger.LogInformation($"Update the patient with following ssn {ssn}");
            try
            {
                await _patientService.UpdatePatient(ssn);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogTrace(ex, $"Patient couldn't be updated with exception message {ex}");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeletePatient")]
        public async Task<IActionResult> DeletePatient(int ssn)
        {
            _logger.LogInformation($"Delete the patient with following ssn {ssn}");
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

        [HttpGet]
        [Route("GetPatient")]
        public async Task<IActionResult> GetPatient(int ssn)
        {
            _logger.LogInformation($"Get the patient with following ssn {ssn}");
            try
            {
                var patient = await _patientService.GetPatient(ssn);
                return Ok(patient);
            }
            catch (Exception ex)
            {
                _logger.LogTrace(ex, $"Patient couldn't be fetched with exception message {ex}");
                return BadRequest(ex.Message);
            }
        }

    }
}
