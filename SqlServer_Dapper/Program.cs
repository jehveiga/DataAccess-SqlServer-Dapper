using Dapper;
using Microsoft.Data.SqlClient;
using SqlServer_Dapper.Models;

// Pacotes instalados
// Microsoft.Data.SqlClient
// Dapper

const string connectionString = "Server=localhost;Database=balta;Integrated Security=true;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True";

using (var connection = new SqlConnection(connectionString))
{
    var categories = connection.Query<Category>("SELECT [Id], [Title] FROM [Category]");

    foreach (var category in categories)
    {
        Console.WriteLine($"{category.Id} - {category.Title}");
    }
}