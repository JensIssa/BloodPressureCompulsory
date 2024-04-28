using AutoMapper;
using PatientApplication;
using PatientApplication.DTO;
using PatientInfrastructure;
using Domain;
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
.AddSource("PatientService")
.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("PatientService"))
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
    config.CreateMap<PatientDTO, PatientBE>();
}).CreateMapper();
builder.Services.AddSingleton(mapper);
#endregion

builder.Services.AddLogging(logBuilder =>
{
    logBuilder.AddSeq("http://seq:5341");
});


#region Depedency Injection
builder.Services.AddDbContext<RepositoryDBContext>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IPatientService, PatientApplication.PatientService>();
#endregion


//builder.Services.AddHostedService<AddMeasurementToPatientHandler>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin", policy =>
    {
        policy.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin();
    });
});

builder.Services.AddHttpClient();


var app = builder.Build();


app.UseCors("AllowAnyOrigin");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
