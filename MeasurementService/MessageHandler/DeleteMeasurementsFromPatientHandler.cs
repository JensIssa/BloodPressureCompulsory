
using EasyNetQ;
using MeasurementApplication.Interfaces;
using Messaging;
using Messaging.SharedMessaging;

namespace MessurementService.MessageHandler
{
    public class DeleteMeasurementsFromPatientHandler : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public DeleteMeasurementsFromPatientHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        private void HandleDeleteMeasurementsFromPatient(DeleteMeasurementsFromPatientSSN measurements)
        {
            Console.WriteLine(measurements.Message);

            try
            {
                using var scope = _serviceProvider.CreateScope();
                var measurementService = scope.ServiceProvider.GetRequiredService<IMeasurementService>();
                measurementService.DeleteMeasurementsByPatientSSNAsync(measurements.PatientSSN);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new ArgumentException("Something went wrong");
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Message Handler is running.....");

            var messageClient = new MessageClient(
                RabbitHutch.CreateBus("host=rabbitmq;port=5672;virtualHost=/;username=guest;password=guest")
            );

            const string topic = "DeleteMeasurementsFromPatient";

            await messageClient.Listen<DeleteMeasurementsFromPatientSSN>(HandleDeleteMeasurementsFromPatient, topic);
        }
    }
}
