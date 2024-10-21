using EverydayIsArtBot.Data;
using EverydayIsArtBot.Helpers;
using Serilog;
using System.Text.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace EverydayIsArtBot.Bot
{
    public class Bot
    {
        private static HttpClient Client = new();
        private readonly ConfigurationHelper _configHelper;

        public Bot(ConfigurationHelper config)
        {
            _configHelper = config;
        }

        public async Task Send()
        {
            string? botToken = _configHelper.GetConfigString("BotToken");
            TelegramBotClient bot = new(botToken);

            Art? art = await GetArt();
            if (art is null)
            {
                return;
            }

            InputFile image = InputFile.FromString(art.ImageUrl);
            string caption = GetMessage(art);

            string? channelName = _configHelper.GetConfigString("ChannelName");
            try
            {
                await bot.SendPhotoAsync(new ChatId(channelName), image, caption: caption, parseMode: ParseMode.Html);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, "An error occurred on post sending.");
            }
        }

        private static string GetMessage(Art art)
        {
            string nl = "\n";
            string message = $"<b>{art.Title}</b>";

            if (art.Date is not null)
            {
                message += $"{nl}{art.Date}";
            }

            if (art.Author is not null)
            {
                message += $"{nl}{nl}{GetStringFromArray(art.Author)}";
            }

            if (art.PlaceOfOrigin is not null)
            {
                message += $"{nl}{nl}{GetStringFromArray(art.PlaceOfOrigin)}";
            }

            if (art.Medium is not null)
            {
                message += $"{nl}{nl}{GetStringFromArray(art.Medium)}";
            }

            if (art.AccessNumber is not null)
            {
                message += $"{nl}{nl}{art.AccessNumber}";
            }

            if (art.WayToGet is not null)
            {
                message += $"{nl}{GetStringFromArray(art.WayToGet)}";
            }

            if (message[^2..] != nl)
            {
                message += nl;
            }
            message += $"{nl}<i><a href='{art.SourceUrl}'>{art.SourceUrlText}</a></i>";

            message = message.Replace($"{nl}{nl}{nl}", $"{nl}{nl}");

            return message;
        }

        private static string GetStringFromArray(string[] arr)
        {
            return string.Join("\n", arr);
        }

        private async Task<Art?> GetArt()
        {
            string artJson = "";
            try
            {
                string apiUrl = _configHelper.GetConfigString("ApiUrl");
                artJson = await Client.GetStringAsync(apiUrl);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, "An error occurred on art receiving.");
            }

            if (artJson == "")
            {
                return null;
            }

            return JsonSerializer.Deserialize<Art>(artJson);
        }
    }
}