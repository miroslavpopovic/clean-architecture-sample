using CleanArchitectureSample.Application;
using CleanArchitectureSample.Infrastructure;
using CleanArchitectureSample.Infrastructure.Data;
using CleanArchitectureSample.Web;
using CleanArchitectureSample.Web.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddKeyVaultIfConfigured(builder.Configuration);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseVersionedSwagger();

app.UseHealthChecks("/health");
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseExceptionHandler(_ => { });

app.Map("/", () => Results.Redirect("/swagger"));

app.MapEndpoints();

app.Run();

public partial class Program { }
