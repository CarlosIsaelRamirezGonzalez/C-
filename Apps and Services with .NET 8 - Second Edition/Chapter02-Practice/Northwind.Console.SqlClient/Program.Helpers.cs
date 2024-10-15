using System.Globalization;
partial class Program
{
    private static void ConfigureConsole(bool useComputerCulture = false) 
    {
        // This enable the special characters in the console, like euro
        OutputEncoding = System.Text.Encoding.UTF8; // Console.System, just System cause the .csproj
        if (!useComputerCulture)
        {
            WriteLine("Select the culture: ");
            WriteLine("     1.- en-US");  
            WriteLine("     2.- es-ES");
            Write("Press a key: ");
            ConsoleKey key = ReadKey().Key;
            Write("\n \n");        
            switch(key)
            {
                case ConsoleKey.D1 or ConsoleKey.NumPad1:
                    CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US"); // System.Globalization
                    break;
                case ConsoleKey.D2 or ConsoleKey.NumPad2:
                    CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("es-ES"); // System.Globalization
                    break;
            }
        }
        WriteLine($"Current Culture Info {CultureInfo.CurrentCulture.DisplayName}");
    }
    private static void WriteLineInColor(string value, ConsoleColor color = ConsoleColor.White)
    {
        ConsoleColor previousColor = ForegroundColor;
        ForegroundColor = color;
        WriteLine(value);
        ForegroundColor = previousColor;
    }
}
