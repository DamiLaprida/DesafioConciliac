using ConciliacDesafio.Domain.Contracts.Repository;
using ConciliacDesafio.Domain.Contracts.Services;
using ConciliacDesafio.Domain.Dtos;
using Microsoft.EntityFrameworkCore;

namespace ConciliacDesafio.Domain.Services
{
    public class TareaService : ITareaService
    {
        private readonly ITareaRepository _tareaRepository;
        public TareaService(ITareaRepository tareaRepository)
        {
            _tareaRepository = tareaRepository;
        }

        public async Task<IEnumerable<TareaDTO>> GetAllTareasAsync()
        {
            var tareas = await _tareaRepository.GetAllTareasAsync();
            return tareas;
        }

        public async Task<TareaDTO> GetTareaByIdAsync(int id)
        {
            var tarea = await _tareaRepository.GetTareaByIdAsync(id);
            return tarea;
        }

        public async Task<TareaDTO> EditarTareaAsync(int id, TareaDTO tareaDTO)
        {
            var tareaEditada = await _tareaRepository.EditarTareaAsync(id, tareaDTO);
            return tareaEditada;
        }

        public async Task<TareaDTO> CambiarTareaPendienteACompletadaAsync(int id)
        {
            var tareaPendienteACompletada = await _tareaRepository.CambiarTareaPendienteACompletadaAsync(id);
            return tareaPendienteACompletada;
        }

        public async Task<TareaDTO> CrearTareaAsync(TareaDTO tareaDTO)
        {
            var tareaCreada = await _tareaRepository.CrearTareaAsync(tareaDTO);
            return tareaCreada;
        }

        public async Task<TareaDTO> BorrarTareaAsync(int id)
        {
            var tareaBorrada = await _tareaRepository.BorrarTareaAsync(id);
            return tareaBorrada;
        }
    }
}
