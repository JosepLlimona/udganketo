using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using udganketo.Services;

namespace udganketo.Pages
{
    public class VotacioModel : PageModel
    {
        private readonly CosmosDbService _cosmosDbService;

        public VotacioModel(CosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        public MyItem item {  get; private set; }

        public async Task OnGetAsync(string id)
        {
            item = await _cosmosDbService.SelectItemAsync(id);
        }

        public async Task<IActionResult> UpdateItemAsync(MyItem updatedItem)
        {
            await _cosmosDbService.UpdateItemAsync(updatedItem);

            return RedirectToPage("/Index");
        }
    }
}
