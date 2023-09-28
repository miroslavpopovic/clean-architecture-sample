using System.Data.Common;

namespace CleanArchitectureSample.Application.FunctionalTests;

public interface ITestDatabase
{
    Task InitializeAsync();

    DbConnection GetConnection();

    Task ResetAsync();

    Task DisposeAsync();
}
