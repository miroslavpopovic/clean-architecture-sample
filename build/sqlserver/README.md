# SQL Server in Docker image

The files here are used to initialize SQL Server in a Docker container, together with some initial logins and a default database.

Add more code to `init.sql` file if necessary.

This code is taken from:
https://www.abhith.net/blog/create-sql-server-database-from-a-script-in-docker-compose/

If you run into an issue with log showing errors in `entrypoint.sh` including `\r` characters, and your `.data/sqlserver` folder is empty, open `entrypoint.sh` and change its line ending from CRLF to LF.
