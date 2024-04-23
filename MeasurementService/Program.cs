using AutoMapper;
using Domain;
using EasyNetQ;
using MeasurementApplication;
using MeasurementApplication.DTO;
using MeasurementApplication.Interfaces;
using MeasurementInfrastructure;
using MeasurementInfrastructure.Interfaces;
using MeasurementRepository;
using Messaging;
using MessurementService.MessageHandler;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region AutoMapper
var mapper = new MapperConfiguration(config =>
{
    config.CreateMap<CreateMeasurementDTO, Measurement>();
    config.CreateMap<UpdateMeasurementDTO, Measurement>();
}).CreateMapper();
builder.Services.AddSingleton(mapper);
#endregion


builder.Services.AddLogging(logBuilder =>
{
    logBuilder.AddSeq("http://seq:5341");
});

#region DependencyInjection
builder.Services.AddDbContext<MeasurementDbContext>();
builder.Services.AddScoped<IMeasurementRepository, MeasurementRepo>();
builder.Services.AddScoped<IMeasurementService, MeasurementCrud>();

#endregion

builder.Services.AddSingleton(new MessageClient(RabbitHutch.CreateBus("host=rabbitmq;port=5672;virtualHost=/;username=guest;password=guest")));

builder.Services.AddHostedService<DeleteMeasurementsFromPatientHandler>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin", policy =>
    {
        policy.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin();
    });
});

var app = builder.Build();

app.UseCors("AllowAnyOrigin");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthorization();

app.MapControllers();

app.Run();
