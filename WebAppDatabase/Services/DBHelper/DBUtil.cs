using Microsoft.Data.SqlClient;

namespace WebAppDatabase.Services.DBHelper;
/// <summary>
/// static --> do not give constructor, this class cannot be instantiated
/// </summary>
public static class DBUtil
{
    public static SqlConnection GetConnection()
    {
        SqlConnection connection;
        ConfigurationBuilder builder = new ConfigurationBuilder();
        builder.AddJsonFile("appsettings.json");
        var configuration = builder.Build();
        string? url = configuration.GetConnectionString("DefaultConnection");

        try
        {
            connection = new SqlConnection(url);
            return connection;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
}