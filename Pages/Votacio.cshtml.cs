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

        public MyItem item { get; private set; }

        public async Task OnGetAsync(string id)
        {
            item = await _cosmosDbService.SelectItemAsync(id);
        }

        [HttpPost] 
        public async Task<IActionResult> OnUpdateVotes(string id, string optionId)
        {

            MyItem item = await _cosmosDbService.SelectItemAsync(id);

            foreach (Options option in item.options)
            {
                if (option.id == optionId)
                {
                    option.votes++;
                    break;
                }
            }

            await _cosmosDbService.UpdateItemAsync(item);

            return RedirectToPage("/Index");
        }
    }
}