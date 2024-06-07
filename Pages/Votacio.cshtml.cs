using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using udganketo.Services;
using static udganketo.Pages.VotacioModel;

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

        public class PostRequestModel
        {
            public string id { get; set; }

            public string optionId { get; set; }
        }

        public async Task<IActionResult> OnPost([FromBody] PostRequestModel viewModel)
        {

            MyItem item = await _cosmosDbService.SelectItemAsync(viewModel.id);

            foreach (Options option in item.options)
            {
                if (option.id == viewModel.optionId)
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