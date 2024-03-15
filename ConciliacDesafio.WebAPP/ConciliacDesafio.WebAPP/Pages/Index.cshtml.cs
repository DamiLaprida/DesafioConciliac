using ConciliacDesafio.Domain.Contracts.Services;
using ConciliacDesafio.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConciliacDesafio.WebAPP.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ITareaService _tareaService;

        public IndexModel(ITareaService tareaService)
        {
            _tareaService = tareaService;
        }

        public IEnumerable<TareaDTO> Tareas { get; set; }

        public async Task OnGetAsync()
        {
            Tareas = await _tareaService.GetAllTareasAsync();
        }
    }
}