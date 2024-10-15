using Microsoft.Data.SqlClient;
using System.Data;

#region Configure Console

WriteLine("Would you like change the default culture of the system?");
WriteLine("     1.- Yes");
WriteLine("     2.- No \n");

Write("Press a key: ");
ConsoleKey key = ReadKey().Key;
Write("\n \n");

switch(key)
{
    case ConsoleKey.D1 or ConsoleKey.NumPad1:
        ConfigureConsole();
        break;

    case ConsoleKey.D2 or ConsoleKey.NumPad2:
        ConfigureConsole(useComputerCulture:true);
        break;

    default:
        WriteLine("Default option: No");
        ConfigureConsole(useComputerCulture:true);
        break;
}
#endregion

#region string builder connection 
WriteLine("Default connection: Local conection");
SqlConnectionStringBuilder builder = Connection_StringBuilder();
#endregion




#region database connection

WriteLine($"Connection String: {builder.ConnectionString}");
SqlConnection connection = new(builder.ConnectionString);
// Events and the events handlers that we made (+= is the operator to suscribe the events to the handlers)
connection.StateChange += Connection_StateChange;
connection.InfoMessage += Connection_InfoMessage;

try
{
    WriteLine("Openning connection. Please wait up to {0} secons...", builder.ConnectTimeout);
    await connection.OpenAsync();
    WriteLine($"SQL Server version: {connection.ServerVersion}");
    connection.StatisticsEnabled = true;
}
catch (Exception ex)
{
    WriteLineInColor($"Something went wrong: {ex.Message}", ConsoleColor.Red);
}

#endregion


#region database interact

Write("Enter a minimun price: ");
string? priceText = ReadLine();
if (!decimal.TryParse(priceText, out decimal price))
{
    WriteLine("You must enter a valid unit price.");
    return;
}

SqlCommand command = connection.CreateCommand();

WriteLine("Execute command using:");
WriteLine("  1 - Text");
WriteLine("  2 - Stored Procedure");
WriteLine();
Write("Press a key: ");
key = ReadKey().Key;
WriteLine();WriteLine();

if (key is ConsoleKey.D1 or ConsoleKey.NumPad1)
{
    command.CommandType = CommandType.Text; // System.Data
    command.CommandText = "SELECT ProductId, ProductName, UnitPrice FROM Products WHERE UnitPrice >= @minimumPrice";
    command.Parameters.AddWithValue("minimumPrice", price); 
} 
else if (key is ConsoleKey.D2 or ConsoleKey.NumPad2)
{
    command.CommandType = CommandType.StoredProcedure;
    command.CommandText = "GetExpensiveProducts"; 
    /*
        CREATE PROCEDURE [dbo].[GetExpensiveProducts]
        @price money,
        @count int OUT
        AS
        PRINT 'Getting expensive products: ' + 
            TRIM(CAST(@price AS NVARCHAR(10)))
        SELECT @count = COUNT(*)
        FROM Products
            WHERE UnitPrice >= @price
        SELECT * 
        FROM Products
        WHERE UnitPrice >= @price
        RETURN 0
    */
    /*This works too: SqlParameter p1 = new(), p2 = new(), p3 = new();*/
    SqlParameter p1 = new() 
    {
        ParameterName = "price",
        SqlDbType = SqlDbType.Money,
        SqlValue = price
    };
    SqlParameter p2 = new() 
    {
        Direction = ParameterDirection.Output,
        ParameterName = "count",
        SqlDbType = SqlDbType.Int
    };
    SqlParameter p3 = new()
    {
        Direction = ParameterDirection.ReturnValue,
        ParameterName = "rv",
        SqlDbType = SqlDbType.Int
    };
    command.Parameters.AddRange(new[] { p1, p2, p3 });
}

SqlDataReader reader = await command.ExecuteReaderAsync(); 
string horizontalLine = new string('-', 60);
WriteLine(horizontalLine);
WriteLine("| {0, 5} | {1, -35} | {2, 10} |",
    arg0: "Id", arg1: "Name", arg2: "Price"); // Se crea la tabla
WriteLine(horizontalLine);


#endregion