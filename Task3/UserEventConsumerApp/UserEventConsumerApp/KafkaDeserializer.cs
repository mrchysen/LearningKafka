using Confluent.Kafka;
using System.Text.Json;

namespace UserEventProducerApp;

public class KafkaDeserializer<T> : IDeserializer<T>
{
    public T Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
    {
        if (isNull)
        {
            return default;
        }

        var elem = JsonSerializer.Deserialize<T>(data);

        return elem;
    }
}
