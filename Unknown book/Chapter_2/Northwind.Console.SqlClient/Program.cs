namespace Northwind.Console.SqlClient;

using System.Data; // CommandType
using Microsoft.Data.SqlClient; // SqlInfoMessageEventArgs
  
partial class Program
{                                  
  static SqlConnection? connection;
 

  static void Main(string[] args)
  { 
    #region makeBuilder
    SqlConnectionStringBuilder builder = new();
    builder.InitialCatalog = "Northwind";
    builder.MultipleActiveResultSets = true;
    builder.Encrypt = true;
    builder.TrustServerCertificate = true;
    builder.ConnectTimeout = 10;

    WriteLine("Connect to: ");
    WriteLine("  1 - SQL Server on local machine");
    WriteLine("  2 - Azure SQL Database");
    WriteLine("  3 – Azure SQL Edge \n");
    Write("Press a key: ");

    ConsoleKey key = ReadKey().Key;
    WriteLine(); WriteLine();

    if (key is ConsoleKey.D1 or ConsoleKey.NumPad1)
    {
        builder.DataSource = "."; // Local SQL Server
        // @".\net7book"; // Local SQL Server with an instance name
    }
    else if (key is ConsoleKey.D2 or ConsoleKey.NumPad2)
    {
        builder.DataSource = // Azure SQL Database
        "tcp:apps-services-net7.database.windows.net,1433"; 
    }
    else if (key is ConsoleKey.D3 or ConsoleKey.NumPad3)
    {
        builder.DataSource = "tcp:127.0.0.1,1433"; // Azure SQL Edge
    }
    else
    {
        WriteLine("No data source selected.");
        return;
    }

    WriteLine("Authenticate using:");
    WriteLine("  1 – Windows Integrated Security");
    WriteLine("  2 – SQL Login, for example, sa");
    WriteLine();
    Write("Press a key: ");
    
    key = ReadKey().Key;
    WriteLine(); WriteLine();
    if (key is ConsoleKey.D1 or ConsoleKey.NumPad1)
    {
        builder.IntegratedSecurity = true;
    }
    else if (key is ConsoleKey.D2 or ConsoleKey.NumPad2)
    {
        builder.UserID = "sa";
        Write("Enter your SQL password: ");
        string? password = ReadLine();
        if (string.IsNullOrWhiteSpace(password))
        {
            WriteLine("The password cannot be null or white space.");
            return;
        }
        builder.Password = password;
        builder.PersistSecurityInfo = false;
    }
    else 
    {
        WriteLine("No autentication selected");
        return;
    }
    #endregion

  //  asincrónicos dentro de Main sincrónico utilizando .GetAwaiter().GetResult(). Esto ejecutará la tarea de forma sincrónica sin bloquear la aplicación. Aquí tienes un ejemplo de cómo puedes hacerlo:

    Connection(builder);
    ConnectionAsync(builder).GetAwaiter().GetResult();

    Write("Enter a unit price: ");
    string? priceText = ReadLine();
    if(!decimal.TryParse(priceText, out decimal price))
    {
      WriteLine("You must enter a valid unit price");
      return;
    }
    ComandSELECT(price);
    // ComandSELECTAsync(price).GetAwaiter().GetResult();

  } 
}