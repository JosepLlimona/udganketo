﻿using Microsoft.Azure.Cosmos;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace udganketo.Services
{
    public class CosmosDbService
    {
        private readonly Container _container;

        public CosmosDbService()
        {
            var cosmosDbConnecxtionString = Environment.GetEnvironmentVariable("COSMOSDB_CONNECTION_STRING");
            Console.WriteLine("COSMOSDB_CONNECTION_STRING: " + cosmosDbConnecxtionString); // Agrega esta línea para imprimir el valor
            var databaseName = "udganketo";
            var containerName = "Polls";
            var cosmosClient = new CosmosClient(cosmosDbConnecxtionString);
            _container = cosmosClient.GetContainer(databaseName, containerName);
        }

        public async Task<List<MyItem>> GetItemsAsync()
        {
            var sqlQueryText = "SELECT * FROM c";
            QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
            FeedIterator<MyItem> queryResultSetIterator = _container.GetItemQueryIterator<MyItem>(queryDefinition);

            List<MyItem> items = new List<MyItem>();

            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<MyItem> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                foreach (var item in currentResultSet)
                {
                    items.Add(item);
                }
            }

            return items;
        }
    }

    public class MyItem
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Question {  get; set; }

        public Options[] Options { get; set; }
        // Otros campos necesarios
    }

    public class Options
    {
        public string Id { get; set; }
        public string name { get; set; }
        public int votes { get; set; }
    }
}