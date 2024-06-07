using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using SignalRChat.Hubs;
using udganketo.Services;

namespace udganketo.Pages
{
    public class VotacioModel : PageModel
    {
        private readonly CosmosDbService _cosmosDbService;
        private readonly IHubContext<UdgAnketoHub> _hubContext;

        public VotacioModel(CosmosDbService cosmosDbService, IHubContext<UdgAnketoHub> hubContext)
        {
            _cosmosDbService = cosmosDbService;
            _hubContext = hubContext;
        }

        [BindProperty]
        public MyItem Item { get; set; }

        [BindProperty]
        public string SelectedOptionId { get; set; }

        public async Task OnGetAsync(string id)
        {
            Item = await _cosmosDbService.SelectItemAsync(id);
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            Item = await _cosmosDbService.SelectItemAsync(id);

            foreach (Options option in Item.options)
            {
                if (option.id == SelectedOptionId)
                {
                    option.votes++;
                    break;
                }
            }

            await _cosmosDbService.UpdateItemAsync(Item);
            await _hubContext.Clients.Group(id).SendAsync("answer", SelectedOptionId);

            return RedirectToPage("/Index");
        }
        /*
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

        public async Task<IActionResult> UpdateItemAsync(string id, string optionId)
        {

            MyItem item = await _cosmosDbService.SelectItemAsync(id);

            foreach(Options option in item.options)
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
        */
    }
}
