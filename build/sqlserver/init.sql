CREATE DATABASE CleanArchitectureSample;
GO
CREATE DATABASE CleanArchitectureSampleTest;
GO

USE CleanArchitectureSample;
GO

CREATE LOGIN CleanArchitectureLogin WITH PASSWORD='Password123', DEFAULT_DATABASE=CleanArchitectureSample;
GO

CREATE USER CleanArchitectureUser FOR LOGIN CleanArchitectureLogin WITH DEFAULT_SCHEMA=dbo;
GO

ALTER ROLE db_owner ADD MEMBER CleanArchitectureUser;
GO

USE CleanArchitectureSampleTest;
GO

CREATE USER CleanArchitectureUser FOR LOGIN CleanArchitectureLogin WITH DEFAULT_SCHEMA=dbo;
GO

ALTER ROLE db_owner ADD MEMBER CleanArchitectureUser;
GO
