using Packt.Shared;

namespace Northwind.Common.EntityModels.Tests;

public class UnitTest1
{
    [Fact]
    public void CanConnectIsTrue()
    {
        using (NorthwindContext db = new()) // arrange
        {
            bool canConnect = db.Database.CanConnect(); // act
            Assert.True(canConnect); // assert
        }
    }

    [Fact]
    public void ProviderIsSqlServer()
    {
        using (NorthwindContext db = new())
        {
            string? provider = db.Database.ProviderName;
            Assert.Equal("Microsoft.EntityFrameworkCore.SqlServer", provider);
        }
    }
    [Fact]
    public void EmployeeHasLastRefreshedIn10sWindow()
    {
        using (NorthwindContext db = new())
        {
            Employee employee1 = db.Employees.Single(p => p.EmployeeId == 1);
            DateTimeOffset now = DateTimeOffset.UtcNow;
            Assert.InRange(actual: employee1.LastRefreshed,
                low: now.Subtract(TimeSpan.FromSeconds(5)),
                high: now.AddSeconds(5));
        }
    }
}