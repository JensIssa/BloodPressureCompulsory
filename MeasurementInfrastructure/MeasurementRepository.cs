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

    public void Rebuild()
    {
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
    }
}