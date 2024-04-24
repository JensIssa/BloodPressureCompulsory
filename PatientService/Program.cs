using AutoMapper;
using PatientApplication;
using PatientApplication.DTO;
using PatientInfrastructure;
using Domain;
using FeatureHubSDK;
using PatientService.FeatureToggle;

var builder = WebApplication.CreateBuilder(args);

FeatureLogging.DebugLogger += (sender, s) => Console.WriteLine("DEBUG: " + s + "\n");
FeatureLogging.TraceLogger += (sender, s) => Console.WriteLine("TRACE: " + s + "\n");
FeatureLogging.InfoLogger += (sender, s) => Console.WriteLine("INFO: " + s + "\n");
FeatureLogging.ErrorLogger += (sender, s) => Console.WriteLine("ERROR: " + s + "\n");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region AutoMapper
var mapper = new MapperConfiguration(config =>
{
    config.CreateMap<PatientDTO, Patient>();
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
builder.Services.AddScoped<IFeatureToggle, FeatureToggle>();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
