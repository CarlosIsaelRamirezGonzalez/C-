namespace Northwind.CosmosDb.Items;

public class CategoryCosmos 
{
    public int categoryId { get; set;}
    public string categoryName { get; set; } = null!; // Not null

    public string? description { get; set; } // Perhaps could be null
}