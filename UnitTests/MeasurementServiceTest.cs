using AutoMapper;
using Domain;
using MeasurementApplication;
using MeasurementApplication.DTO;
using MeasurementInfrastructure.Interfaces;
using Moq;

namespace UnitTests;

public class MeasurementServiceTest
{
    private readonly MeasurementCrud _service;
    private readonly Mock<IMeasurementRepository> _measurementRepositoryMock = new Mock<IMeasurementRepository>();
    private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();

    public MeasurementServiceTest()
    {
        _service = new MeasurementCrud(_measurementRepositoryMock.Object, _mapperMock.Object);
    }
    
    [Fact]
    public async Task AddMeasurementAsync()
    {
        // Arrange
        var createMeasurementDto = new CreateMeasurementDTO
        {
            Systolic = 120,
            Diastolic = 80,
            PatientSSN = "12345"
        };

        var expectedMeasurement = new Measurement
        {
            Systolic = createMeasurementDto.Systolic,
            Diastolic = createMeasurementDto.Diastolic,
            PatientSSN = createMeasurementDto.PatientSSN
        };

        _mapperMock.Setup(m => m.Map<Measurement>(It.IsAny<CreateMeasurementDTO>())).Returns(expectedMeasurement);
        _measurementRepositoryMock.Setup(r => r.AddMeasurementAsync(It.IsAny<Measurement>())).ReturnsAsync(expectedMeasurement);
        
        // Act
        var result = await _service.AddMeasurementAsync(createMeasurementDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(createMeasurementDto.Systolic, result.Systolic);
        Assert.Equal(createMeasurementDto.Diastolic, result.Diastolic);
        Assert.Equal(createMeasurementDto.PatientSSN, result.PatientSSN);
        Assert.False(result.IsSeen);
        Assert.Equal(DateTime.Now.Date, result.Date.Date); 
        _measurementRepositoryMock.Verify(r => r.AddMeasurementAsync(It.IsAny<Measurement>()), Times.Once);

    }

    [Fact]
    public async Task DeleteMeasurementById_validId()
    {
        // Arrange
        int validId = 1;
        _measurementRepositoryMock.Setup(r => r.DeleteMeasurementByIdAsync(validId)).Returns(Task.CompletedTask);

        // Act
        await _service.DeleteMeasurementById(validId);

        // Assert
        _measurementRepositoryMock.Verify(r => r.DeleteMeasurementByIdAsync(validId), Times.Once);

    }

    [Fact]
    public async Task DeleteMeasurementById_InvalidId()
    {
        // Arrange
        int invalidId = 0;

        // Act & Assert
        ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(
            () => _service.DeleteMeasurementById(invalidId)
        );

        Assert.Equal("Id cannot be less than 1", exception.Message);
    }
}