using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Application.Interfaces;
using Application.Interfaces.Infrastructure.MQTT;
using Application.Models.Dtos.Checking;
using HiveMQtt.Client.Events;
using HiveMQtt.MQTT5.Types;

namespace Infrastructure.MQTT.EventHandlers;

public class CheckingEventHandler(ICheckingService checkingService, IMqttPublisher publisher) : IMqttMessageHandler
{
    public string TopicFilter => "access";
    public QualityOfService QoS => QualityOfService.AtLeastOnceDelivery;

    public async Task Handle(object? sender, OnMessageReceivedEventArgs args)
    {
        var dto = JsonSerializer.Deserialize<CheckBookingRequestDto>(args.PublishMessage.PayloadAsString,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        if (dto == null)
            throw new Exception("Failed to parse CheckBookingRequestDto");

        var context = new ValidationContext(dto);
        Validator.ValidateObject(dto, context);

        var check = new CheckBookingRequestDto
        {
            Rfid = dto.Rfid,
            ServiceId = dto.ServiceId
        };

        var response = await checkingService.CheckIfValid(check);

        try
        {
            await publisher.Publish(response, "access/response");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"MQTT publish error: {ex.Message}");
        }
    }
}