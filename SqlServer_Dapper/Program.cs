using Dapper;
using Microsoft.Data.SqlClient;
using SqlServer_Dapper.Models;

// Pacotes instalados
// Microsoft.Data.SqlClient
// Dapper

const string connectionString = "Server=localhost;Database=balta;Integrated Security=true;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True";

var category = new Category();
category.Id = Guid.NewGuid();
category.Title = "Amazon AWS";
category.Url = "amazon";
category.Summary = "AWS Cloud";
category.Order = 8;
category.Description = "Categoria destinada a serviços do AWS";
category.Featured = false;

// Não concatenar string para passagem de parametro
var insertSql = @"INSERT INTO  
                       [Category] 
                  VALUES(
                       @Id, 
                       @Title, 
                       @Url, 
                       @Summary, 
                       @Order, 
                       @Description, 
                       @Featured)";

using (var connection = new SqlConnection(connectionString))
{
    // Executando o insert conforme a ordem dos parametro referidos acima
    var rowsAffected = connection.Execute(insertSql, new
    {
        category.Id,
        category.Title,
        category.Url,
        category.Summary,
        category.Order,
        category.Description,
        category.Featured
    });

    Console.WriteLine($"{rowsAffected} linhas inseridas");

    var categories = connection.Query<Category>("SELECT [Id], [Title] FROM [Category]");

    foreach (var item in categories)
    {
        Console.WriteLine($"{item.Id} - {item.Title}");
    }
}