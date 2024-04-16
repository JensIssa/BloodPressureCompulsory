using MeasurementApplication.Interfaces;
using MeasurementInfrastructure.Interfaces;

namespace MeasurementApplication;

public class MeasurementCrud : IMeasurementService
{
    private readonly IMeasurementRepository _measurementRepository;
    
    public MeasurementCrud(IMeasurementRepository measurementRepository)
    {
        _measurementRepository = measurementRepository;
    }
    
    public void Rebuild()
    {
        _measurementRepository.Rebuild();
    }
}