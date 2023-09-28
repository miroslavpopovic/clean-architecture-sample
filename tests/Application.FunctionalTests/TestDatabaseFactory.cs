namespace CleanArchitectureSample.Application.FunctionalTests;

public static class TestDatabaseFactory
{
    public static async Task<ITestDatabase> CreateAsync()
    {
        var database = new TestcontainersTestDatabase();

        await database.InitializeAsync();

        return database;
    }
}
