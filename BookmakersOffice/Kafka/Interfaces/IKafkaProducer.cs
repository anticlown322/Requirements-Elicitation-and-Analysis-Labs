using Confluent.Kafka;

namespace Kafka.Interfaces;

/// <summary>
/// Interfaces for all kafka message producers(microservices) 
/// </summary>
public interface IKafkaProducer
{
    Task ProduceAsync(string topic, Message<string, string> message);
    //its better to do serializers for each message type but use <string, string> 
    //for first try
}