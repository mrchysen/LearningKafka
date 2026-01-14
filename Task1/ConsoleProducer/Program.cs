using Confluent.Kafka;
using static System.Console;

var topicName = "messages";

var config = new ProducerConfig
{
    BootstrapServers = "localhost:9092"
};

using var producer = new ProducerBuilder<Null, string>(config).Build();

while (true)
{
    Write("Enter message to topic: ");

    var message = ReadLine() ?? string.Empty;

    await producer.ProduceAsync(topicName, new Message<Null, string> { Value = message });

    WriteLine();
}