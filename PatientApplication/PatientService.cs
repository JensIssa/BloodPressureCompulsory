using AutoMapper;
using Azure.Core;
using Domain;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PatientApplication.DTO;
using PatientInfrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PatientApplication
{
    public class PatientService : IPatientService
    {

        private readonly IPatientRepository _repository;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<PatientService> _logger;
        private readonly HttpClient _client;

        public PatientService(IPatientRepository repository, IMapper mapper, IHttpClientFactory httpClientFactory, ILogger<PatientService> logger)
        {
            _repository = repository;
            _mapper = mapper; 
            _httpClientFactory = httpClientFactory;
            _client = _httpClientFactory.CreateClient();
            _logger = logger;
        }

        public async Task<Patient> AddPatient(PatientDTO patient)
        {
            var patientDTO = _mapper.Map<Patient>(patient);

            var addPatient = await _repository.CreatePatient(patientDTO);
            
            return addPatient;
        }

        public void RebuildDb()
        {
            _repository.RebuildDb();
        }

        public async Task<List<GetPatientDTO>> GetAllPatients()
        {
            var patients = await _repository.GetAllPatients();
            var patientsDTO = new List<GetPatientDTO>();

            foreach (var patient in patients)
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"http://measurementservice:8080/Measurement/GetByPatientSSN/{patient.SSN}");
                _logger.LogInformation("Sending request to API.");

                var response = await _client.SendAsync(request);

                _logger.LogInformation($"Received response: {response.StatusCode}");
                var measurements = new List<Measurement>();

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    measurements = JsonConvert.DeserializeObject<List<Measurement>>(result);
                }

                var patientDTO = new GetPatientDTO
                {
                    SSN = patient.SSN,
                    Name = patient.Name,
                    Email = patient.Email,
                    Measurements = measurements
                };

                patientsDTO.Add(patientDTO);
            }

            return patientsDTO;
        }


        public async Task UpdatePatient(string ssn, PatientDTO patient)
        {
            await _repository.UpdatePatient(ssn, _mapper.Map<Patient>(patient));
        }

        public async Task DeletePatient(string ssn)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete , $"http://measurementservice:8080/Measurement/DeleteByPatientSSN/{ssn}");
           _logger.LogInformation("Sending request to API for mass deletions of measurements.");
            await _client.SendAsync(request);

            await _repository.DeletePatient(ssn);
        }

        public async Task<GetPatientDTO> GetPatient(string ssn)
        {
            var patient = await _repository.GetPatient(ssn);
            var request = new HttpRequestMessage(HttpMethod.Get, $"http://measurementservice:8080/Measurement/GetByPatientSSN/{ssn}");
            _logger.LogInformation("Sending request to API.");

            var response = await _client.SendAsync(request);

            _logger.LogInformation($"Received response: {response.StatusCode}");
            var Measurements = new List<Measurement>();

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                Measurements = JsonConvert.DeserializeObject<List<Measurement>>(result);
            }

            var patientRequest = new GetPatientDTO
            {
                SSN = patient.SSN,
                Name = patient.Name,
                Email = patient.Email,
                Measurements = Measurements
            };

            return patientRequest;
        }
    }
}
