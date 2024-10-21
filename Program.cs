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

if (args.Length == 0)
{
    Console.WriteLine("Enter the frequency argument of post publication in minutes.");
}

bool isSuccess = int.TryParse(args[0], out int freqMinutes);
if (!isSuccess)
{
    Console.WriteLine("The frequency argument entered in incorrect format.");
}

ConfigurationHelper helper = new(configuration);
Bot bot = new(helper);

while (true)
{
    await bot.Send();
    Thread.Sleep(freqMinutes * 1000 * 60);
}