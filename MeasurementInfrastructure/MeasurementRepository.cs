using Domain;
using MeasurementInfrastructure;
using MeasurementInfrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

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

    public async Task DeleteMeasurementByIdAsync(int measurementId)
    {
        var measurementToDelete = await _context.Measurements.FirstOrDefaultAsync(m => m.Id == measurementId);
        _context.Measurements.Remove(measurementToDelete);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteMeasurementsByPatientSSNAsync(string patientSSN)
    {
        var measurements = await _context.Measurements
            .Where(m => m.PatientSSN == patientSSN)
            .ToListAsync();

        if (measurements.Any())
        {
            _context.Measurements.RemoveRange(measurements);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Measurement>> GetMeasurementsByPatientSSNAsync(string patientSSN)
    {
        return await _context.Measurements
          .Where(m => m.PatientSSN == patientSSN)
          .ToListAsync();
    }

    public async Task MarkMeasurementAsSeenAsync(int measurementId)
    {
        var measurement = await _context.Measurements.FindAsync(measurementId);
        if (measurement != null)
        {
            measurement.IsSeen = true;
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new KeyNotFoundException("Measurement not found.");
        }
    }

    public void Rebuild()
    {
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
    }

    public async Task UpdateMeasurementAsync(int measurementId, Measurement updatedMeasurement)
    {
        var measurementToUpdate = await _context.Measurements.FirstOrDefaultAsync(m => m.Id == updatedMeasurement.Id);

        if (measurementId != updatedMeasurement.Id)
        {
            throw new ArgumentException("ids does not match");
        }

        measurementToUpdate.Diastolic = updatedMeasurement.Diastolic;
        measurementToUpdate.Systolic = updatedMeasurement.Systolic;
        _context.Measurements.Update(measurementToUpdate);
        await _context.SaveChangesAsync();
    }
}