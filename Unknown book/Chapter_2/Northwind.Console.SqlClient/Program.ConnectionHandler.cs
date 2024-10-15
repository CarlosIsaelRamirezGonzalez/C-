namespace Northwind.Console.SqlClient;

using Microsoft.Data.SqlClient; // SqlInfoMessageEventArgs
using System.Data;

partial class Program 
{




    static void Connection(SqlConnectionStringBuilder builder)
    {
        connection = new(builder.ConnectionString);
        WriteLine(connection.ConnectionString);
        WriteLine();
        connection.StateChange += Connection_StateChange;
        connection.InfoMessage += Connection_InfoMessage;
        try 
        {
            WriteLine("Opening connection. Please wait up to {0} seconds.....", builder.ConnectTimeout);
            WriteLine();
            connection.Open();
            WriteLine($"SQL Server version: {connection.ServerVersion}");
            connection.StatisticsEnabled = true;
        }
        catch(SqlException ex)
        {
            WriteLine($"SQL exception: {ex.Message}");
           
        }
    }
     

    static async Task ConnectionAsync(SqlConnectionStringBuilder builder)
    {
        
        connection = new(builder.ConnectionString);

        WriteLine(connection.ConnectionString);
        WriteLine();

        connection.StateChange += Connection_StateChange;
        connection.InfoMessage += Connection_InfoMessage;

        WriteLine("Opening connection. Please wait up to {0} seconds.....", builder.ConnectTimeout);
        WriteLine();
        try 
        {
            await connection.OpenAsync();

            WriteLine($"SQL Server version: {connection.ServerVersion}");

            connection.StatisticsEnabled = true;
        }    
        catch(SqlException ex)
        {
            WriteLine($"SQL exception: {ex.Message}");
            return;
        }
    }
}