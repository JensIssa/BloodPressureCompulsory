using AutoMapper;
using Domain;
using Messaging;
using Messaging.SharedMessaging;
using PatientApplication.DTO;
using PatientInfrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientApplication
{
    public class PatientService : IPatientService
    {

        private readonly IPatientRepository _repository;
        private readonly IMapper _mapper;
        private readonly MessageClient _messageClient;

        public PatientService(IPatientRepository repository, IMapper mapper, MessageClient messageClient)
        {
            _repository = repository;
            _mapper = mapper; 
            _messageClient = messageClient;
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

        public async Task<List<Patient>> GetAllPatients()
        {
            return await _repository.GetAllPatients();
        }

        public async Task UpdatePatient(string ssn, PatientDTO patient)
        {
            await _repository.UpdatePatient(ssn, _mapper.Map<Patient>(patient));
        }

        public async Task DeletePatient(string ssn)
        {
           await _messageClient.Send(new DeleteMeasurementsFromPatientSSN($"Deleting Measurements from Patient: {ssn}", ssn), "DeleteMeasurementsFromPatient");

           await _repository.DeletePatient(ssn);
        }

        public Task<Patient> GetPatient(string ssn)
        {
            return _repository.GetPatient(ssn);
        }

        public async Task AddMeasurementToPatient(Measurement measurement)
        {
            await _repository.AddMeasurementToPatient(measurement);
        }
    }
}
