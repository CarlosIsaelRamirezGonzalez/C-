// #nullable disable


// <copyright file="Program.cs" company="Packt">
// Copyright (c) Packt. All rights reserved.
// </copyright>
namespace CodeAnalyzing;

using Packt.Shared;

/// <summary>
/// The main class for this console app.
/// </summary>
public class Program
{    /// <summary>
        /// Describe el propósito de este campo.
    /// </summary>
    public static Stream? s; 

  /// <summary>
  /// The main entry point for this console app.
  /// </summary>
  /// <param name="args">
  /// A string array of arguments passed to the console app.
  /// </param>
  public static void Main(string[] args)
  {
    #region switch expressions
    string message = s switch
    {
        FileStream writeableFile when s.CanWrite => "The stream is a file that I can write to.",
        FileStream readOnlyFile => "The stream is a read-only file.",
        MemoryStream ms => "The stream is a memory address.",
        null => "The stream is null.",
        _ => "The stream is some other type."
    };
    #endregion
    #region Work with index
    #region traditional form to hand an index
    int index = 2;
    String[] names = {"Carlos", "Pepe", "Pedro", "Meli"};
    String name = names[index];
    char letter = name[index];
    #endregion
    #region Index form to hand an index
    // two ways to define the same index, 3 in from the start 
    Index i1 = new Index(value: 3); // counts from the start 
    Index i2 = 3; // using implicit int conversion operator
    // two ways to define the same index, 5 in from the end
    Index i3 = new Index(value: 5, fromEnd: true);  // counts in reverse, from front to back
    Index i4 = ^5; // using the caret operator
    #endregion
    #endregion
    #region Work with ranges
    Range r1 = new Range(start: new Index(3), end: new Index(7));
    Range r2 = new Range(start: 3, end: 7); // using implicit int conversion
    Range r3 = 3..7; // using C# 8.0 or later syntax
    Range r4 = Range.StartAt(3); // from index 3 to last index
    Range r5 = 3..; // from index 3 to last index
    Range r6 = Range.EndAt(3); // from index 0 to index 3
    Range r7 = ..3; // from index 0 to index 3
    #endregion
    #region readonly in methods
    ImmutablePerson jeff = new()
    {
    FirstName = "Jeff", // allowed
    LastName = "Winger"
    };
    // jeff.FirstName = "Geoff"; // compile error!
    #endregion
    #region Raw string literals
    String jsonName = "Carlos";
    int age = 12;
    String xml =  """
             <person age="50">
               <first_name>Mark</first_name>
             </person>
             """;
    string json = $$"""
            {
            "first_name": "{{jsonName}}",
            "age": {{age}},
            "xml": {{xml}}
            };
            """;
    
    #endregion
    #region Requiring properties
    Book book = new() { Isbn = "1234-5678"};
    book.Title = "C# 11 and .NET 7 - Modern Cross-Platform Development";
    #endregion

  } 
}