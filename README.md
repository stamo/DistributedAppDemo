# DistributedAppDemo
This is a demo project for my talk Between Microservices and Monolith at .Networking Conference 2025

Prerequisites:

1. [Docker Desktop](https://www.docker.com/products/docker-desktop/)
2. [Dapr Cli](https://docs.dapr.io/getting-started/install-dapr-cli/)
3. [Dapr extension for Visual Studio](https://marketplace.visualstudio.com/items?itemName=ms-azuretools.vs-dapr)

To load configuration in Redis you have to run these commands:

``` bash
docker exec dapr_redis redis-cli SET HallsConnectionString "Host=localhost;Database=cinema_halls;User Id={0};Password={1};Port=5432;"

```

``` bash
docker exec dapr_redis redis-cli SET AuditConnectionString "Host=localhost;Database=cinema_audit;User Id={0};Password={1};Port=5432;"

```

In folders Cinema.Audit and Cinema.HallManager you have to create file secrets.json with this content:

``` json
{
  "DbUser": "user_name",
  "DbPassword": "password"
}

```
To use Entity Framework Tooling you have to add your connection string to User Secrets like this:

``` json
{
  "ConnectionStrings": {
    "AuditDbConnection": "Host=localhost;Database=cinema_audit;User Id=user_name;Password=password;Port=5432;"
  }
}

```

Only projects Cinema.ApiGateway, Cinema.Audit and Cinema.HallManager are partialy implemented. Other projects are just empty shells which you can try to implement