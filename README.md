# Authentication and Authorization

## Creating the secret for JWT
Create user secret by right clicking on the project and then selecting 'Manage User Secrets'. Then structure the json
content as follow:
```
{
  "JwtSecurityToken": {
    "Key": "<enter a base64 encoded key here. can be generated from git bash using command: 'openssl rand -base64 32'"
  }
}
```

## Run the project
### Migrate Database
- Update the database connection string in the `appsettings.json`(or `appsettings.Development.json`) to point to a MSSQL database.
- Install Entity Framework tool(if it is not already installed): `dotnet tool install --global dotnet-ef`.
- Run `dotnet ef dbcontext info` to check if the database connection string is in proper shape.
- Run `dotnet ef database update` to apply the migrations and create the tables.
- Run `dotnet run` to run the project.

### Initial Admin user
An admin user is created automatically at the first time run of the project.
- Email: admin@email.com
- Password: 1234

On the call to `/api/user/register` route, new customers will be created.

Admin and customer can login using the `/api/user/login` route.

Test authentication and authorization using the other two routes.

[Find in Postman](https://api.postman.com/collections/15225143-71bdbec4-4cbe-44c6-ab88-0b6c5e22e38b?access_key=PMAT-01HDCAWM156SJYDVPM82XP3VB9)
