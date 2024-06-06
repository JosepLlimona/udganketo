using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyFirstAzureWebApp.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly CosmosDbService _cosmosDbService;

    public IndexModel(ILogger<IndexModel> logger, CosmosDbService cosmosDbService)
    {
        _logger = logger;
        _cosmosDbService = cosmosDbService;
    }

    public List<MyItem> Items { get; private set; }

    public async Task OnGetAsync()
    {
        Items = await _cosmosDbService.GetItemsAsync();
    }

    public void OnGet()
    {

    }
}
