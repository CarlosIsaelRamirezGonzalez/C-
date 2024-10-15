using Microsoft.Azure.Cosmos; // CosmosClient, DatabaseResponse, Database, IndexingPolicy, and so on
using System.Net; // HttpStatusCode
using Packt.Shared; // NorthwindContext, Products, Category, and so on
using Northwind.CosmosDb.Items; //  ProductCosmos, CategoryCosmos, and so on
using Microsoft.EntityFrameworkCore;  // Include a extension method
partial class Program
{
    // to use Azure Cosmos DB in the local emulator
    private static string endpointUri = "https://localhost:8081/";
    private static string primaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
    static async Task CreateCosmosResources()
    {
        SectionTitle("Creating Cosmos resources");
        try 
        {
            using (CosmosClient client = new(
                accountEndpoint: endpointUri,
                authKeyOrResourceToken: primaryKey))
            {
                DatabaseResponse dbResponse = await client.CreateDatabaseIfNotExistsAsync("Northwind", throughput: 400 /* RU/s */);

                string status = dbResponse.StatusCode switch
                {
                    HttpStatusCode.OK => "exists", // 200
                    HttpStatusCode.Created => "Created", // 201
                    _ => "unknown"
                };

                WriteLine("Database Id: {0}, Status: {1}.", arg0: dbResponse.Database.Id, arg1: status);

                // This object define how index the data inside the container, like how indexing the dat, and waht kind of path will have 
                IndexingPolicy indexingPolicy = new()
                {
                    IndexingMode = IndexingMode.Consistent,
                    Automatic = true,
                    IncludedPaths = {new IncludedPath { Path = "/*" }}
                };

                ContainerProperties containerProperties = new("Products", partitionKeyPath: "/productId")
                {
                    IndexingPolicy = indexingPolicy
                };

                ContainerResponse containerResponse = await dbResponse.Database.CreateContainerIfNotExistsAsync(containerProperties, throughput: 1000 /* RU/s */);

                status = dbResponse.StatusCode switch
                {
                    HttpStatusCode.OK => "exists",
                    HttpStatusCode.Created => "created",
                    _ => "unknown"
                };

                WriteLine("Container Id: {0}, Status: {1}.", arg0: containerResponse.Container.Id, arg1: status);

                Container container = containerResponse.Container;
                ContainerProperties properties = await container.ReadContainerAsync();
                
                WriteLine($"    PartitionKeyPath: {properties.PartitionKeyPath}");
                WriteLine($"    LastModified: {properties.LastModified}");
                WriteLine("    IndexingPolicy.IndexingMode: {0}", arg0: properties.IndexingPolicy.IndexingMode);
                WriteLine("    IndexingPolicy.IncludePaths: {0}", arg0: string.Join(",", properties.IndexingPolicy.IncludedPaths.Select(path => path.Path)));
                WriteLine($"    IndexingPolicy: {properties.IndexingPolicy}");            
            }
        }            
        catch(HttpRequestException ex)
        {
            WriteLine("Error: {0}", arg0: ex.Message);
            WriteLine("Hint: Make sure the Azure Cosmos Emulator is running.");
        } 
        catch(Exception ex)
        {
            WriteLine("Error: {0} says{1}", arg0: ex.GetType(), arg1: ex.Message);  
        }
    }

    static async Task CreateProductItems()
    {
        SectionTitle("Creating product items");
        double totalCharge = 0.0;
        try 
        {
            using(CosmosClient client = new(
                accountEndpoint: endpointUri,
                authKeyOrResourceToken: primaryKey
            ))
            {
                Container container = client.GetContainer(databaseId: "Northwind", containerId: "Products");
                using (NorthwindContext db = new())
                {
                    ProductCosmos[] products = db.Products
                    // get the related data for embedding
                    .Include(p => p.Category)
                    .Include(p => p.Supplier)
                    // filter any products with null category or supplier
                    // to avoid null warnings
                    .Where(p => (p.Category != null) && (p.Supplier != null))
                    // project the EF Core entities into Cosmos JSON types
                    .Select(p => new ProductCosmos
                    {
                        id = p.ProductId.ToString(),
                        productId = p.ProductId.ToString(),
                        productName = p.ProductName,
                        quantityPerUnit = p.QuantityPerUnit,
                        unitPrice = p.UnitPrice,
                        unitsInStock = p.UnitsInStock,
                        unitsOnOrder = p.UnitsOnOrder,
                        reorderLevel = p.ReorderLevel,
                        discontinued = p.Discontinued,
                        category = new CategoryCosmos
                        {
                            categoryId = p.Category != null ? p.Category.CategoryId : 0,
                            categoryName = p.Category != null ? p.Category.CategoryName : string.Empty,
                            description = p.Category != null ? p.Category.Description : string.Empty
                        },
                        supplier = new SupplierCosmos
                        {
                            supplierId = p.Supplier != null ? p.Supplier.SupplierId : 0,
                            companyName = p.Supplier != null ? p.Supplier.CompanyName : string.Empty,
                            contactName = p.Supplier != null ? p.Supplier.ContactName : string.Empty,
                            contactTitle = p.Supplier != null ? p.Supplier.ContactTitle : string.Empty,
                            address = p.Supplier != null ? p.Supplier.Address : string.Empty,
                            city = p.Supplier != null ? p.Supplier.City : string.Empty,
                            region = p.Supplier != null ? p.Supplier.Region : string.Empty,
                            postalCode = p.Supplier != null ? p.Supplier.PostalCode : string.Empty,
                            country = p.Supplier != null ? p.Supplier.Country : string.Empty,
                            phone = p.Supplier != null ? p.Supplier.Phone : string.Empty,
                            fax = p.Supplier != null ? p.Supplier.Fax : string.Empty,
                            homePage = p.Supplier != null ? p.Supplier.HomePage : string.Empty
                        }
                    })
                    .ToArray();
                    foreach (ProductCosmos product in products)
                    {
                        try
                        {
                            ItemResponse<ProductCosmos> productResponse = await container.ReadItemAsync<ProductCosmos>(id: product.id, new PartitionKey(product.productId));
                            WriteLine("Item with id: {0} exists. Query consumed {1} RUs.", arg0: productResponse.Resource.id, productResponse.RequestCharge);
                            totalCharge += productResponse.RequestCharge;
                        }
                        catch (CosmosException ex)
                            when(ex.StatusCode == HttpStatusCode.NotFound) // Cuando no existe entonces lo creamos
                        {
                            ItemResponse<ProductCosmos> productResponse = await container.CreateItemAsync(product);
                            WriteLine("Created item with id: {0}. Insert consumed {1} RUs.", productResponse.Resource.id, productResponse.RequestCharge);
                            totalCharge += productResponse.RequestCharge;
                        }
                        catch (Exception ex)
                        {
                            WriteLine("WrError: {0} says {1}", arg0: ex.GetType(), arg1: ex.Message);
                        }
                    }
                }
            }
        }
        catch (HttpRequestException ex)
        {
            WriteLine("Error {0}", arg0: ex.Message);
            WriteLine("Hint: Make sure the Azure Cosmos Emulator is running.");
        }
        catch (Exception ex)
        {
            WriteLine("Error: {0} says {1}", arg0: ex.GetType(), arg1: ex.Message);
        }
        WriteLine("Total requests charge: {0:N2} RUs", totalCharge);
    }

}