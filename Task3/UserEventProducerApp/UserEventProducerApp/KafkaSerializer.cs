using Confluent.Kafka;
using System.Text.Json;

namespace UserEventProducerApp;

public class KafkaSerializer<T> : ISerializer<T>
{
    public byte[] Serialize(T data, SerializationContext context)
    {
        return JsonSerializer.SerializeToUtf8Bytes(data);
    }
}
