using Dapper;
using Microsoft.Data.SqlClient;
using SqlServer_Dapper.Models;

// Pacotes instalados
// Microsoft.Data.SqlClient
// Dapper

const string connectionString = "Server=localhost;Database=balta;Integrated Security=true;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True";


using (var connection = new SqlConnection(connectionString))
{
    UpdateCategory(connection);

    //CreateCategory(connection);

    ListCategories(connection);
}

static void ListCategories(SqlConnection connection)
{
    var categories = connection.Query<Category>("SELECT [Id], [Title] FROM [Category]");

    foreach (var item in categories)
    {
        Console.WriteLine($"{item.Id} - {item.Title}");
    }
}

static void CreateCategory(SqlConnection connection)
{
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
}

static void UpdateCategory(SqlConnection connection)
{
    var updateQuery = "UPDATE [Category] SET [Title] = @title WHERE [Id] = @id";

    var rowsAffected = connection.Execute(updateQuery, new
    {
        id = new Guid("af3407aa-11ae-4621-a2ef-2028b85507c4"),
        title = "Frontend 2023"
    });

    Console.WriteLine($"{rowsAffected} registros atualizados");
}