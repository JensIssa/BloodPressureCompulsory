using Domain;
using MeasurementInfrastructure;
using MeasurementInfrastructure.Interfaces;

namespace MeasurementRepository;

public class MeasurementRepo : IMeasurementRepository
{
    private readonly MeasurementDbContext _context;
    public MeasurementRepo(MeasurementDbContext context)
    {
        _context = context;    
    }

    public async Task<Measurement> AddMeasurementAsync(Measurement measurement)
    {
        _context.Measurements.Add(measurement);
        await _context.SaveChangesAsync();
        return measurement;
    }

    public void Rebuild()
    {
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
    }
}