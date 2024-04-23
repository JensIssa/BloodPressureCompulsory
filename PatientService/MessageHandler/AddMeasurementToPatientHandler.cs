
using Domain;
using Domain.DTO;
using EasyNetQ;
using Messaging;
using Messaging.SharedMessaging;
using PatientApplication;

namespace PatientService.MessageHandler
{
    public class AddMeasurementToPatientHandler : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public AddMeasurementToPatientHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        private async void HandleAddMeasurementToPatient(AddMeasurementToPatient measurement)
        {
            Console.WriteLine(measurement.Message);

            try
            {
                using var scope = _serviceProvider.CreateScope();
                var patientService = scope.ServiceProvider.GetRequiredService<IPatientService>();
                var realMeasurement = new Measurement
                {
                    Id = measurement.MeasurementID,
                    PatientSSN = measurement.PatientSSN
             
                };

                await patientService.AddMeasurementToPatient(realMeasurement);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Message Handler is running....");

            var messageClient = new MessageClient(
                RabbitHutch.CreateBus("host=rabbitmq;port=5672;virtualHost=/;username=guest;password=guest")
                );

            const string topic = "AddMeasurementToPatient";

            await messageClient.Listen<AddMeasurementToPatient>(HandleAddMeasurementToPatient, topic);
        }
    }
}
