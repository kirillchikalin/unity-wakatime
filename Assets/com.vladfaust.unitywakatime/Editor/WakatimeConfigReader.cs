using System;
using System.IO;
using UnityEngine;

namespace Editor
{
/// <summary>
/// Generated with ChatGPT
/// </summary>
public static class WakatimeConfigReader
{
    public static string GetApiUrl()
    {
        string homePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        string configPath = Path.Combine(homePath, ".wakatime.cfg");

        if (!File.Exists(configPath))
        {
            Debug.LogWarning($".wakatime.cfg not found at {configPath}");
            return null;
        }

        try
        {
            var lines = File.ReadAllLines(configPath);
            bool inSettingsSection = false;

            foreach (var rawLine in lines)
            {
                string line = rawLine.Trim();

                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#") || line.StartsWith(";"))
                    continue;

                if (line.StartsWith("[") && line.EndsWith("]"))
                {
                    inSettingsSection = line.Equals("[settings]", StringComparison.OrdinalIgnoreCase);
                    continue;
                }

                if (inSettingsSection && line.StartsWith("api_url"))
                {
                    var parts = line.Split(new char[] { '=' }, 2);
                    if (parts.Length == 2)
                    {
                        return parts[1].Trim();
                    }
                }
            }

            Debug.LogWarning("api_url not found in [settings] section");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to read wakatime config: {ex.Message}");
        }

        return null;
    }
}
}