using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using SignalRChat.Hubs;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            await _hubContext.Clients.Group(id).SendAsync("updateItem", Item);
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
            await _hubContext.Clients.Group(id).SendAsync("updateItem", Item);

            return RedirectToPage("/Index");
        }
    }
}