param (
    [string]$name
)

dotnet ef migrations add $name --output-dir Data/Migrations --project ./src/Infrastructure/Infrastructure.csproj --startup-project ./src/Web/Web.csproj --msbuildprojectextensionspath ./artifacts/obj/Web
