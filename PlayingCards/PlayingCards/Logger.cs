using System;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace PlayingCards;

public static class Logger
{
    public static ILogger Log { get; } = new LoggerConfiguration()
                                        .Enrich.With<Indents>()
                                        .WriteTo.File("PlayingCards.log",
                                                      outputTemplate: $"[{{Timestamp:HH:mm:ss}} {{Level:u3}}]{{Indents}}{{Message:l}}{Environment.NewLine}{{Exception}}")
                                        .CreateLogger();

    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public static int Indents { get; set; }
}

public class Indents : ILogEventEnricher
{
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory) => logEvent.AddOrUpdateProperty(propertyFactory.CreateProperty("Indents", "".PadLeft(Logger.Indents)));
}