using Confluent.Kafka;
using static System.Console;

var groupId = GetConsumerGroup(args);
var topicName = "logs";

WriteLine($"GroupId: {groupId}");

var config = new ConsumerConfig
{
    GroupId = groupId,
    BootstrapServers = "localhost:9092",

    AutoOffsetReset = AutoOffsetReset.Earliest
};

using var c = new ConsumerBuilder<Ignore, string>(config).Build();

c.Subscribe(topicName);

while (true)
{
    try
    {
        var cr = c.Consume();
        WriteLine($"Consumed message '{cr.Message.Value}' at: '{cr.TopicPartitionOffset}'.");

        SaveMessageToLogFile(cr.Message.Value);
    }
    catch (ConsumeException e)
    {
        WriteLine($"Error occured: {e.Error.Reason}");
    }
}

static void SaveMessageToLogFile(string message)
{
    using var sw = new StreamWriter("logs.txt", true);

    sw.WriteLine(message);

    sw.Close();
}

static string GetConsumerGroup(string[] args)
{
    if (args.Length == 0)
    {
        return "consumer-group-1";
    }

    return args[0];
}