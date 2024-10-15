namespace Northwind.Console.SqlClient;

using Microsoft.Data.SqlClient; // SqlInfoMessageEventArgs
using System.Data;

partial class Program 
{
    static void ComandSELECT(decimal price)
    {
        if (connection == null)
        {
            WriteLine("The connection cannot be null.");
            return;
        }    
        ConsoleKey key = new();
        WriteLine("Execute command using:");
        WriteLine("  1 - Text");
        WriteLine("  2 - Stored Procedure");
        WriteLine();
        Write("Press a key: ");
        key = ReadKey().Key;
        WriteLine(); WriteLine();

        SqlCommand cmd = connection.CreateCommand();
        SqlParameter p1 = new(),p2 = new(), p3 = new();

        if(key is ConsoleKey.D1 or ConsoleKey.NumPad1)
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT ProductId, ProductName, UnitPrice FROM Products WHERE UnitPrice > @price";
            cmd.Parameters.AddWithValue("price", price);
        }
        else if(key is ConsoleKey.D2 or ConsoleKey.NumPad2)
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetExpensiveProducts";
            p1 = new()
            {
                ParameterName = "price",
                SqlDbType = SqlDbType.Money,
                SqlValue = price
            };
            p2 = new()
            {
                Direction = ParameterDirection.Output,
                ParameterName = "count",
                SqlDbType = SqlDbType.Int
            };
            p3 = new() 
            {
                Direction = ParameterDirection.ReturnValue,
                ParameterName = "rv",
                SqlDbType = SqlDbType.Int
            };
            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            cmd.Parameters.Add(p3);
        }

        SqlDataReader reader = cmd.ExecuteReader();
        
        WriteLine("----------------------------------------------------------");
        WriteLine("| {0,5} | {1,-35} | {2,8} |", "Id", "Name", "Price");
        WriteLine("----------------------------------------------------------");
        
        while(reader.Read())
        {
            WriteLine("| {0, 5} | {1, -35} | {2, 8:C} |",
                        reader.GetInt32("ProductId"),
                        reader.GetString("ProductName"),
                        reader.GetDecimal("UnitPrice"));
        }

        WriteLine("----------------------------------------------------------");
        reader.Close();
        WriteLine($"Output count: {p2.Value}");
        WriteLine($"Return value: {p3.Value}");
        connection.Close();
    }

    static async Task ComandSELECTAsync(decimal price)
    {
        if (connection == null)
        {
            WriteLine("The connection cannot be null.");
            return;
        }
        SqlCommand cmd = connection.CreateCommand();
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "SELECT ProductId, ProductName, UnitPrice FROM Products WHERE UnitPrice > @price";
        cmd.Parameters.AddWithValue("price", price);

        SqlDataReader reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            WriteLine("| {0,5} | {1,-35} | {2,8:C} |",
            await reader.GetFieldValueAsync<int>("ProductId"),
            await reader.GetFieldValueAsync<string>("ProductName"),
            await reader.GetFieldValueAsync<decimal>("UnitPrice"));
        }
        WriteLine("----------------------------------------------------------");
        await reader.CloseAsync();
        await connection.CloseAsync();

    }

}