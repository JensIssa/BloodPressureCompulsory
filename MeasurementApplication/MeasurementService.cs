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

    public async Task<Measurement> AddMeasurementAsync(MeasurementDTO measurementDto)
    {
        var measurement = _mapper.Map<Measurement>(measurementDto);
        measurement.Date = DateTime.Now; 
        measurement.IsSeen = false; 

        var addedMeasurement = await _measurementRepository.AddMeasurementAsync(measurement);
        return addedMeasurement; 
    }

    public void Rebuild()
    {
        _measurementRepository.Rebuild();
    }
}