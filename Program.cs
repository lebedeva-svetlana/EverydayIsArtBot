using EverydayIsArtBot.Bot;
using EverydayIsArtBot.Helpers;
using Microsoft.Extensions.Configuration;
using Serilog;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

//Log.CloseAndFlush();

ConfigurationHelper helper = new(configuration);
Bot bot = new(helper);
await bot.Send();