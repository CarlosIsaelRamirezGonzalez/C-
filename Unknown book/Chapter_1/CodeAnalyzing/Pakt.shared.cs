namespace Packt.Shared;

/// <summary>
/// Description for the class
/// </summary>
public class ImmutablePerson
{
    /// <summary>
    /// Description for the attribute
    /// </summary>
    public string? FirstName { get; init; }
    /// <summary>
    /// Description for the attribute
    /// </summary>
    public string? LastName { get; init; }

    /// <summary>
    /// Description for the method
    /// </summary>
    public void Hire(String manager, String employee)
    {
        ArgumentNullException.ThrowIfNull(manager);
        ArgumentNullException.ThrowIfNull(employee);
    }


}

/// <summary>
/// Description for the class
/// </summary>
public record ImmutableAnimal(string Name, string Species);

/// <summary>
/// Description for the class
/// </summary>
public class Book 
{
    /// <summary>
    /// Description for the attribute
    /// </summary>
    public required string Isbn {get; set;}
    /// <summary>
    /// Description for the attribute
    /// </summary>
    public string? Title {get; set;}
    /// <summary>
    /// Description for the method
    /// </summary>
    public Book(String Isbn)     
    {
        this.Isbn = Isbn;
    }
    /// <summary>
    /// Description for the method
    /// </summary>
    public Book()     
    {
    }

}



