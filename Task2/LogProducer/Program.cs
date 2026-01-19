using Confluent.Kafka;
using static System.Console;

var appName = GetAppName(args);

var topicName = "logs";

var config = new ProducerConfig
{
    BootstrapServers = "localhost:9092"
};

WriteLine($"Start to send logs to topic {topicName}");

using var producer = new ProducerBuilder<Null, string>(config).Build();

while (true)
{
    var rnd = new Random();

    var logLevel = rnd.Next(1, 3) switch
    {
        1 => "Error",
        2 => "Info",
        3 => "Warning",
        _ => "Info"
    };

    var logMessage = rnd.Next(1, 4) switch
    {
        1 => "Main web controller is sending message",
        2 => "Kafka workflow procces works",
        3 => "Customer is searching for goods",
        _ => "Debug log"
    };

    var message = $"{appName} [{logLevel}] {logMessage}";

    await producer.ProduceAsync(topicName, new Message<Null, string> { Value = message });

    WriteLine("Message sent: " + message);

    await Task.Delay(1000 * rnd.Next(1, 4)); 
}

static string GetAppName(string[] args)
{
    if (args.Length == 0)
        return "StandartAppName";

    return args[0]; 
}