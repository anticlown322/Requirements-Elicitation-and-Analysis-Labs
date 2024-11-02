using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using PaymentMicroservice.Business.Models;
using PaymentMicroservice.Business.Services;
using PaymentMicroservice.Data.Entities;

namespace Kafka.Consumers;

public class TransactionKafkaConsumer(IServiceScopeFactory scopeFactory) : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.Run(() =>
        {
            _ = ConsumeAsync("transactionTopic", stoppingToken);
        }, stoppingToken);
    }
    
    public async Task ConsumeAsync(string topic, CancellationToken stoppingToken)
    {
        var consumerConfig = new ConsumerConfig() // create separated configs
        {
            GroupId = "transaction-consume-group",
            BootstrapServers = "localhost:9092", // store in config file
            AutoOffsetReset = AutoOffsetReset.Earliest,
            Acks = Acks.All
        };

        using var consumer = new ConsumerBuilder<string, string>(consumerConfig).Build();
        consumer.Subscribe("transactionTopic"); // store in config file

        while (!stoppingToken.IsCancellationRequested)
        {
            var consumedData = consumer.Consume(TimeSpan.FromSeconds(3));

            if (consumedData is not null)
            {
                TransactionModel? transactionModel =
                    JsonConvert.DeserializeObject<TransactionModel>(consumedData.Message.Value);

                TransactionEntity transactionEntity = new()
                {
                    AccountId = transactionModel.AccountId,
                    Amount = transactionModel.Amount,
                    Type = transactionModel.Type,
                    TransactionDateTime = transactionModel.TransactionDateTime
                };

                var scope = scopeFactory.CreateScope();
                var transactionService = scope.ServiceProvider.GetRequiredService<ITransactionService>();

                await transactionService.Create(transactionEntity);
            }
        }

        consumer.Close();
    }
}