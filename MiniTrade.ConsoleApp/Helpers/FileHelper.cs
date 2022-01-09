using System.Text;
using System.Text.Json;

namespace MiniTrade.ConsoleApp.Helpers
{
    internal static class FileHelper
    {
        public static void SaveStringToFile(string relativePath, string data)
        {
            string outputPath = "cache/sample-cache.json";

            string fullPath = Path.GetFullPath(relativePath);

            string? directoryName = Path.GetDirectoryName(fullPath);

            if ((directoryName != null) && (!Directory.Exists(directoryName)))
                Directory.CreateDirectory(directoryName);

            using (StreamWriter sw = new StreamWriter(relativePath, false, Encoding.UTF8))
            {
                sw.AutoFlush = true;
                sw.Write(data);
            }

        }

        internal static async Task<string> LoadFileAsync(string relativePath)
        {
            string fullPath = Path.GetFullPath(relativePath);

            string? directoryName = Path.GetDirectoryName(fullPath);

            if (!Directory.Exists(directoryName))
                return string.Empty;

            using (StreamReader sr = new StreamReader(fullPath))
            {
                return await sr.ReadToEndAsync();
            }
        }

        internal static async Task<T?> LoadFileAsync<T>(string outputPath) where T : class
        {
            string json = await FileHelper.LoadFileAsync(outputPath);

            if (string.IsNullOrEmpty(json))
                return null;

            return JsonSerializer.Deserialize<T>(json);
        }

        internal static void SaveToFile<T>(string relativePath, T accounts)
        {
            string outputPath = "cache/sample-cache.json";

            string fullPath = Path.GetFullPath(relativePath);

            string? directoryName = Path.GetDirectoryName(fullPath);

            if ((directoryName != null) && (!Directory.Exists(directoryName)))
                Directory.CreateDirectory(directoryName);

            using (StreamWriter sw = new StreamWriter(relativePath, false, Encoding.UTF8))
            {
                sw.AutoFlush = true;

                var data = JsonSerializer.Serialize<T>(accounts);

                sw.Write(data);
            }
        }
    }
}
