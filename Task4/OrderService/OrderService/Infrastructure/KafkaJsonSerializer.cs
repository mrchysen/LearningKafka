using Confluent.Kafka;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OrderService.Infrastructure;

public class KafkaJsonSerializer<T> : ISerializer<T>
{
    private readonly JsonSerializerOptions _options = new JsonSerializerOptions
    {
        Converters = { new JsonStringEnumConverter() }
    };

    public byte[] Serialize(T data, SerializationContext context)
    {
        return JsonSerializer.SerializeToUtf8Bytes(data, _options);
    }
}
