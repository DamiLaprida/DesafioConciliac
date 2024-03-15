using ConciliacDesafio.Domain.Contracts.Services;
using ConciliacDesafio.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ConciliacDesafio.WebAPP.Controllers
{
    [ApiController]
    [Route("api/tareas")]
    public class TareasController : ControllerBase
    {
        private readonly ITareaService _tareaService;

        public TareasController(ITareaService tareaService)
        {
            _tareaService = tareaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTareasAsync()
        {
            var tareas = await _tareaService.GetAllTareasAsync();
            return Ok(tareas);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetTareaByIdAsync(int id)
        {
            var tarea = await _tareaService.GetTareaByIdAsync(id);
            if (tarea is null)
                return NotFound("No se ha encontrado la tarea solicitada.");

            return Ok(tarea);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> EditarTareaAsync(int id, [FromBody] TareaDTO tareaDTO)
        {
            var tareaEditada = await _tareaService.EditarTareaAsync(id, tareaDTO);
            if (tareaEditada is null)
                return BadRequest("No se ha podido editar la tarea solicitada.");

            return Ok(tareaEditada);
        }

        [HttpPut("Completada/{id:int}")]
        public async Task<IActionResult> CambiarTareaPendienteACompletadaAsync(int id)
        {
            var tareaPendienteACompletada = await _tareaService.CambiarTareaPendienteACompletadaAsync(id);
            if (tareaPendienteACompletada is null)
                return BadRequest("No se ha podido cambiar la tarea a completada.");

            return Ok(tareaPendienteACompletada);
        }

        [HttpPost]
        public async Task<IActionResult> CrearTareaAsync([FromBody] TareaDTO tareaDTO)
        {
            if (String.IsNullOrEmpty(tareaDTO.Titulo))
                return BadRequest("El campo Título no puede estar vacío.");

            if (String.IsNullOrEmpty(tareaDTO.Descripcion))
                return BadRequest("El cambio Descripción no puede estar vacío.");

            var tareaCreada = await _tareaService.CrearTareaAsync(tareaDTO);
            if (tareaCreada is null)
                return BadRequest("No se ha podido crear la tarea solicitada.");

            return Ok(tareaCreada);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> BorrarTareaAsync(int id)
        {
            var tareaBorrada = await _tareaService.BorrarTareaAsync(id);
            if (tareaBorrada is null)
                return BadRequest("No se ha podido borrar la tarea solicitada. Quizás esta ya no exista.");

            return Ok(tareaBorrada);
        }
    }
}
