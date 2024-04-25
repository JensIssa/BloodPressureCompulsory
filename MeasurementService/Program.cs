using AutoMapper;
using Domain;
using MeasurementApplication;
using MeasurementApplication.DTO;
using MeasurementApplication.Interfaces;
using MeasurementInfrastructure;
using MeasurementInfrastructure.Interfaces;
using MeasurementRepository;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region 
builder.Services.AddOpenTelemetry().WithTracing(builder => builder
.AddAspNetCoreInstrumentation()
.SetSampler(new AlwaysOnSampler())
.AddSource("MeasurementService")
.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("MeasurementService"))
.AddZipkinExporter(options =>
{
    options.Endpoint = new Uri("http://zipkin:9411/api/v2/spans");
}));

#endregion

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .Enrich.FromLogContext()
    .WriteTo.Seq("http://seq:5341")
    .WriteTo.Console()
    .CreateLogger();

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
