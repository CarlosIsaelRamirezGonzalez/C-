using System.Globalization; // To use CultureInfo
using Microsoft.Data.SqlClient; // Use SQL Connection
using System.Collections; // To use IDictionary


partial class Program 
{
    private static void ConfigureConsole(string culture = "en-US", bool useComputerCulture = false)
    {
        // To enable Unicode characters like Euro symbol in the console.
        OutputEncoding = System.Text.Encoding.UTF8; // Usariamos Console.OutputEncoding si no fuer apor <Using Include="System.Console" Static="true" /> dentro del csproj
        if (!useComputerCulture)
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo(culture);
        }
        WriteLine($"CurrentCulture: {CultureInfo.CurrentCulture.DisplayName}");
    }
    private static void WriteLineInColor(string value, ConsoleColor color = ConsoleColor.White)
    {
        ConsoleColor previousColor = ForegroundColor;
        ForegroundColor = color;
        WriteLine(value);
        ForegroundColor = previousColor;
    }

    // Statistics
    private static void OutputStatistics(SqlConnection conenction)
    {
        // Remove all the strings values to see all the statistics
        string[] includeKeys = {
            "BytesSent", "BytesReceived", "ConnectionTime", "SelectRows" 
        };
        IDictionary statistics = conenction.RetrieveStatistics();
        foreach (object? key in statistics.Keys)
        {
            if (!includeKeys.Any() || includeKeys.Contains(key))
            {
                if (int.TryParse(statistics[key]?.ToString(), out int value))
                {
                    WriteLineInColor($"{key}: {value:N0}", ConsoleColor.Cyan);
                }
            }
        }
    }
}