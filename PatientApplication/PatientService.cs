using AutoMapper;
using Domain;
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

        public PatientService(IPatientRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper; 
        
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

        public async Task<Patient> UpdatePatient(int ssn)
        {
            return await _repository.UpdatePatient(ssn);
        }

        public Task<Patient> DeletePatient(int ssn)
        {
            return _repository.DeletePatient(ssn);
        }

        public Task<Patient> GetPatient(int ssn)
        {
            return _repository.GetPatient(ssn);
        }
    }
}
