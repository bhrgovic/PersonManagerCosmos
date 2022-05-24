using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CosmosBrunoHrgovic.Dao
{
    public static class CosmosDbServiceProvider
    {
        private const string DatabaseName = "Person";
        private const string ContainerName = "Person";
        private const string Account = "https://cosmoshrgovic.documents.azure.com:443/";
        private const string Key = "A2DuevvjW4EfYzAfpEZMzXQqCZe3xZq1R4rs0zXuqNuMIOgiJzxVkpeAgm7Ruxsl3vyYrbt4dmapTtghPFjqCw==";
        private static ICosmosDbService cosmosDbService;

        public static ICosmosDbService CosmosDbService { get => cosmosDbService; }

        public async static Task Init()
        {
            CosmosClient client = new CosmosClient(Account, Key);
            cosmosDbService = new CosmosDbService(client, DatabaseName, ContainerName);
            DatabaseResponse database = await client.CreateDatabaseIfNotExistsAsync(DatabaseName);
            await database.Database.CreateContainerIfNotExistsAsync(ContainerName, "/id");
        }
    }
}