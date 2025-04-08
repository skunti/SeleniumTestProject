using System.Text.Json;
using Microsoft.Extensions.Configuration;
using System.IO;
using System;
using System.Collections.Generic;

namespace SeleniumTestProject.Utils
{
    public static class ConfigManager
    {
        private static readonly IConfigurationRoot config;

        static ConfigManager()
        {
            try
            {
                string rootPath = Directory.GetCurrentDirectory() + "/Configuration/";
                string envFilePath = Path.Combine(rootPath, "env.json");

                Console.WriteLine("🔍 Looking for env.json config at: " + envFilePath);

                if (!File.Exists(envFilePath))
                {
                    throw new FileNotFoundException("🚫 'env.json' file not found!", envFilePath);
                }

                string envJsonContent = File.ReadAllText(envFilePath);
                var envJson = JsonSerializer.Deserialize<Dictionary<string, string>>(envJsonContent);

                string environment = envJson != null && envJson.ContainsKey("Environment") && !string.IsNullOrWhiteSpace(envJson["Environment"])
                    ? envJson["Environment"]
                    : "dev"; // default fallback

                Console.WriteLine($"✅ Using environment: {environment}");

                var builder = new ConfigurationBuilder()
                    .SetBasePath(rootPath)
                    .AddJsonFile($"appsettings.{environment}.json", optional: false, reloadOnChange: true);

                config = builder.Build();
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ ConfigManager initialization failed: " + ex.Message);
                throw;
            }
        }

        public static string Get(string key)
        {
            var value = config[key];
            if (string.IsNullOrWhiteSpace(value))
            {
                Console.WriteLine($"⚠️ Config key '{key}' not found or empty.");
            }
            return value;
        }
    }
}
