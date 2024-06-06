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
        }
        public string Title { get; set; }
        public string Description {  get; set; }
        public string Question {  get; set; }
        public List<Options> Options { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            int lastId = await _cosmosDbService.GetLastItemIdAsync();

            // Generar el pr�ximo ID increment�ndolo en uno
            int nextId = lastId + 1;
            var newItem = new MyItem
            {

                Id = nextId.ToString(),
                Title = Title,
                Description = Description,
                Question = Question,
                Options = Options
            };

            await _cosmosDbService.InsertItemAsync(newItem);

            return RedirectToPage("/Index");
        }

    }
}
