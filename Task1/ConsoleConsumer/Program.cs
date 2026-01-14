using Confluent.Kafka;
using static System.Console;

var groupId = GetConsumerGroup(args);
var topicName = "messages";

WriteLine($"GroupId: {groupId ?? "consumer-group-1"}");

var config = new ConsumerConfig
{
    GroupId = groupId ?? "consumer-group-1",
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
    }
    catch (ConsumeException e)
    {
        WriteLine($"Error occured: {e.Error.Reason}");
    }
}

static string? GetConsumerGroup(string[] args)
{
    if(args.Length > 0)
    {
        return args[0];
    }

    return null;
}