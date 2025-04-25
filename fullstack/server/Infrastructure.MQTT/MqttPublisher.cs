using System.Text.Json;
using Application.Interfaces.Infrastructure.MQTT;
using HiveMQtt.Client;
using HiveMQtt.MQTT5.Types;

namespace Infrastructure.MQTT;

public class MqttPublisher(HiveMQClient client) : IMqttPublisher
{
    public async Task Publish(object dto, string topic)
    {
        
          JsonSerializerOptions options = new(JsonSerializerDefaults.Web)
            {
                WriteIndented = false
            };

        await client.PublishAsync(topic, JsonSerializer.Serialize(dto,options), QualityOfService.AtLeastOnceDelivery);
    }
}