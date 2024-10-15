using Microsoft.Data.SqlClient;

partial class Program
{
    public static SqlConnectionStringBuilder Connection_StringBuilder()
    {
        SqlConnectionStringBuilder builder = new()
        {
            InitialCatalog = "Northwind",
            MultipleActiveResultSets = true,
            Encrypt = true,
            TrustServerCertificate = true,
            ConnectTimeout = 10,
            IntegratedSecurity = true,
            DataSource = "."
        };
        return builder;
    }
}
