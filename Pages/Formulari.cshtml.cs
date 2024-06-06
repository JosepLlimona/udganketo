using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using udganketo.Services;

namespace udganketo.Pages
{
    public class FormulariModel : PageModel
    {
        private readonly CosmosDbService _cosmosDbService;

        public FormulariModel(CosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
            Options = new List<Options>();
        }
        public string Title { get; set; }
        public string Description {  get; set; }
        public string Question {  get; set; }
        public List<Options> Options { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            int lastId = await _cosmosDbService.GetLastItemIdAsync();

            // Generar el próximo ID incrementándolo en uno
            int nextId = lastId + 1;
            var newItem = new MyItem
            {
                id = nextId.ToString(),
                title = Title,
                description = Description,
                question = Question,
                options = Options
            };

            await _cosmosDbService.InsertItemAsync(newItem);

            return RedirectToPage("/Index");
        }

    }
}
