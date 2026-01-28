using Confluent.Kafka;
using System.Windows;
using UserEventProducerApp;
using static Confluent.Kafka.ConfigPropertyNames;

public class Program
{
    public const string Topic = "logs";

    [STAThread]
    public static void Main(string[] args)
    {
        var producerConfig = new ProducerConfig
        {
            BootstrapServers = "localhost:9092",
        };

        using var producer = 
            new ProducerBuilder<string, UserEvent>(producerConfig)
            .SetValueSerializer(new KafkaSerializer<UserEvent>())
            .Build();

        var app = new Application();

        app.Startup += (s, e) =>
        {
            var mainWindow = new MainWindow(GetUserId(args), producer, Topic);
            mainWindow.Show();
        };

        app.Run();
    }

    private static string GetUserId(string[] args)
    {
        if (args.Length > 0)
            return args[0];
        return "";
    }
}