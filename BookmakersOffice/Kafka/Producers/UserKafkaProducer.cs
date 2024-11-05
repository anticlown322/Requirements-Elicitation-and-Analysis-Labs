using Confluent.Kafka;
using Kafka.Interfaces;

namespace Kafka.Producers;

public class UserKafkaProducer : IKafkaProducer
{
    private readonly IProducer<string, string> _producer;

    public UserKafkaProducer()
    {
        ProducerConfig config = new()
        {
            BootstrapServers = "localhost:9092"  , //better bring it to config file
            Acks = Acks.All
        };

        _producer = new ProducerBuilder<string, string>(config).Build();
    }

    public Task ProduceAsync(string topic, Message<string, string> message)
    {
        return _producer.ProduceAsync(topic, message);
    }
}