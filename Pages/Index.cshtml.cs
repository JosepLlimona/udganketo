using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging; // Asegúrate de agregar este using si no lo tienes
using System.Collections.Generic;
using System.Threading.Tasks;

namespace udganketo.Pages
{
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
}
