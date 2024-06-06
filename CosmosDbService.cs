using Microsoft.Azure.Cosmos;
using System.Collections.Generic;
using System.Threading.Tasks;

public class CosmosDbService
{
    private readonly CosmosClient _cosmosClient;
    private readonly Container _container;

    public CosmosDbService(CosmosClient cosmosClient)
    {
        _cosmosClient = cosmosClient;
        _container = _cosmosClient.GetContainer("udganketo", "Polls");
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
    // Otros campos necesarios
}
