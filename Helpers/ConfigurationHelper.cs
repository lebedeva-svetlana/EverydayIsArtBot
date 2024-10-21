using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EverydayIsArtBot.Helpers
{
    public class ConfigurationHelper
    {
        private readonly IConfiguration _config;

        public ConfigurationHelper(IConfiguration config)
        {
            _config = config;
        }

        public string? GetConfigString(string key)
        {
            string? value = _config.GetValue<string>(key);
            if (key is null || key == "")
            {
                throw new ArgumentNullException(key);
            }
            return value;
        }
    }
}