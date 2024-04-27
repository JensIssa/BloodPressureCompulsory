using AutoMapper;
using Domain;
using MeasurementApplication.DTO;
using MeasurementApplication.Interfaces;
using MeasurementInfrastructure.Interfaces;


namespace MeasurementApplication;

public class MeasurementCrud : IMeasurementService
{
    private readonly IMeasurementRepository _measurementRepository;
    private readonly IMapper _mapper;
    
    public MeasurementCrud(IMeasurementRepository measurementRepository, IMapper mapper)
    {
        _measurementRepository = measurementRepository;
        _mapper = mapper;
    }

    public async Task<Measurement> AddMeasurementAsync(CreateMeasurementDTO measurementDto)
    {
        var measurement = _mapper.Map<Measurement>(measurementDto);
        measurement.Date = DateTime.Now; 
        measurement.IsSeen = false; 

        var addedMeasurement = await _measurementRepository.AddMeasurementAsync(measurement);

        return addedMeasurement; 
    }

    public async Task DeleteMeasurementById(int measurementId)
    {
        if (measurementId < 1)
        {
            throw new ArgumentException("Id cannot be less than 1");
        }

        await _measurementRepository.DeleteMeasurementByIdAsync(measurementId);
    }

    public async Task DeleteMeasurementsByPatientSSNAsync(string patientSSN)
    {
        await _measurementRepository.DeleteMeasurementsByPatientSSNAsync(patientSSN);
    }

    public async Task<ICollection<Measurement>> GetMeasurementsByPatientSSNAsync(string patientSSN)
    {
        return await _measurementRepository.GetMeasurementsByPatientSSNAsync(patientSSN);
    }

    public async Task MarkMeasurementAsSeenAsync(int measurementId)
    {
        if (measurementId < 1)
        {
            throw new ArgumentException("The id cannot be less than 1");
        }

        await _measurementRepository.MarkMeasurementAsSeenAsync(measurementId);
    }



    public void Rebuild()
    {
        _measurementRepository.Rebuild();
    }

    public async Task UpdateMeasurementAsync(int measurementId, UpdateMeasurementDTO measurement)
    {
        if (measurementId != measurement.Id)
        {
            throw new ArgumentException("The ids must match");
        }

        if (measurementId < 1)
        {
            throw new ArgumentException("The id cannot be less than 1");
        }

        await _measurementRepository.UpdateMeasurementAsync(measurementId, _mapper.Map<Measurement>(measurement));
    }
}