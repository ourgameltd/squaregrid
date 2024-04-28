﻿using Azure.Data.Tables;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SquareGrid.Common.Services.Tables.Models;

namespace OurGame.Common
{
    public static class DependencyRegistration
    {
        public static IServiceCollection RegisterCommonDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["BlobStorageConnection"];
            var blobServiceClient = new BlobServiceClient(connectionString);
            var containerClient = blobServiceClient.GetBlobContainerClient("media");
            containerClient.CreateIfNotExists();
            services.AddSingleton(blobServiceClient);

            var tableServiceClient = new TableServiceClient(connectionString);
            var tableGameClient = tableServiceClient.GetTableClient(nameof(SquareGridGame));
            tableGameClient.CreateIfNotExists();
            var tableBlockClient = tableServiceClient.GetTableClient(nameof(SquareGridBlock));
            tableBlockClient.CreateIfNotExists();
            var tableClients = new Dictionary<string, TableClient>();
            tableClients.TryAdd(nameof(SquareGridGame), tableGameClient);
            tableClients.TryAdd(nameof(SquareGridBlock), tableBlockClient);
            services.AddSingleton(new TableManager(tableClients));
            return services;
        }
    }
}