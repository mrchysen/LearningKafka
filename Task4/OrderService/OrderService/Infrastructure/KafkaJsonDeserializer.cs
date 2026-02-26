using Confluent.Kafka;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OrderService.Infrastructure;

public class KafkaJsonDeserializer<T> : IDeserializer<T>
{
    private readonly JsonSerializerOptions _options = new JsonSerializerOptions
    {
        Converters = { new JsonStringEnumConverter() }
    };

    public T? Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
    {
        return JsonSerializer.Deserialize<T>(data, _options);
    }
}
