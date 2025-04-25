using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using ms_cards.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text.Json;
using System.Text;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;


namespace ms_cards.Services;

public class ConsumerService : BackgroundService
{
    private readonly IServiceProvider _services;
    private readonly IConfiguration _config;
    private IConnection? _connection;
    private IModel? _channel;

    public ConsumerService(IServiceProvider services, IConfiguration config)
    {
        _services = services;
        _config = config;
    }

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("🔁 Iniciando servicio de consumidor de eventos...");
        return base.StartAsync(cancellationToken);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory
        {
            Uri = new Uri(_config["RabbitMQ:Url"]!),
            DispatchConsumersAsync = true
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        var queue = _config["RabbitMQ:Queue"];

        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.Received += async (_, ea) =>
        {
            try
            {
                var json = Encoding.UTF8.GetString(ea.Body.ToArray());
                Console.WriteLine($"📩 Mensaje recibido: {json}");

                var evento = JsonSerializer.Deserialize<Evento>(json);
                if (evento == null)
                {
                    Console.WriteLine("⚠️ No se pudo deserializar el evento.");
                    return;
                }

                if (evento.Tipo != "usuario_creado")
                {
                    Console.WriteLine($"ℹ️ Evento ignorado: tipo '{evento.Tipo}'");
                    return;
                }

                var u = evento.Data;

                var tarjeta = new Tarjeta
                {
                    UserId = u.UserId,
                    NombreCompleto = $"{u.Nombre} {u.Apellido}",
                    CardNumber = "400012341234" + new Random().Next(1000, 9999),
                    CVV = new Random().Next(100, 999),
                    MesExp = new Random().Next(1, 12),
                    AñoExp = DateTime.Now.Year + 5
                };

                using var scope = _services.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                db.Tarjetas.Add(tarjeta);
                var saved = await db.SaveChangesAsync(stoppingToken);

                if (saved > 0)
                    Console.WriteLine($"✅ Tarjeta creada para usuario {u.UserId}");
                else
                    Console.WriteLine($"⚠️ Ninguna tarjeta fue guardada para usuario {u.UserId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error procesando mensaje: " + ex.Message);
            }
        };

        _channel.BasicConsume(queue, true, consumer);
        return Task.CompletedTask;
    }

    public override void Dispose()
    {
        _channel?.Close();
        _connection?.Close();
        base.Dispose();
    }
}

public class Evento
{
    [JsonPropertyName("tipo")]
    public string Tipo { get; set; } = "";

    [JsonPropertyName("data")]
    public Usuario Data { get; set; } = new();
}

public class Usuario
{
    [JsonPropertyName("user_id")]
    public int UserId { get; set; }

    [JsonPropertyName("nombre")]
    public string Nombre { get; set; } = "";

    [JsonPropertyName("apellido")]
    public string Apellido { get; set; } = "";

    [JsonPropertyName("email")]
    public string Email { get; set; } = "";
}
