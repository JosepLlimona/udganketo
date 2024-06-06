using Microsoft.Azure.Cosmos;
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

        public async Task<int> GetLastItemIdAsync()
        {
            var query = new QueryDefinition("SELECT TOP 1 VALUE MAX(c.id) FROM c");
            var iterator = _container.GetItemQueryIterator<int>(query, requestOptions: new QueryRequestOptions
            {
                MaxItemCount = 1 // Solo necesitamos obtener el primer elemento
            });

            if (iterator.HasMoreResults)
            {
                var result = await iterator.ReadNextAsync();
                return result.FirstOrDefault(); // Devuelve el primer (y único) ID
            }

            return 0; // Si no hay resultados, devuelve 0
        }

        public async Task InsertItemAsync(MyItem newItem)
        {
            await _container.CreateItemAsync(newItem);
        }
    }

    public class MyItem
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Question {  get; set; }

        public List<Options> Options { get; set; }
        // Otros campos necesarios
    }

    public class Options
    {
        public string Id { get; set; }
        public string name { get; set; }
        public int votes { get; set; }
    }
}