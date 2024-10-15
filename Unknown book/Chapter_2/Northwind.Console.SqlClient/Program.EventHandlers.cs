namespace Northwind.Console.SqlClient;

using Microsoft.Data.SqlClient; // SqlInfoMessageEventArgs
using System.Data;

partial class Program 
{
    /*En el caso específico que muestras, Connection_StateChange, es probable que se esté siguiendo una convención de eventos de .NET. A menudo, al suscribirse a eventos en .NET, se utiliza el nombre del objeto seguido por el nombre del evento, separados por un guion bajo (_). Este es un patrón común en .NET Framework.*/
    static void Connection_StateChange(object sender, StateChangeEventArgs e)
    {
        ConsoleColor previousColor = ForegroundColor;
        ForegroundColor = ConsoleColor.DarkYellow;
        WriteLine($"State change from {e.OriginalState} to {e.CurrentState}");
        ForegroundColor = previousColor;
    }
    
    static void Connection_InfoMessage(Object sender, SqlInfoMessageEventArgs e)
    {
        ConsoleColor previousColor = ForegroundColor;
        ForegroundColor = ConsoleColor.DarkBlue;
        WriteLine($"Info: {e.Message}.");
        foreach(SqlError error in e.Errors) 
        {
            WriteLine($"Error: {error.Message}.");
        }
        ForegroundColor = previousColor;
    }

}