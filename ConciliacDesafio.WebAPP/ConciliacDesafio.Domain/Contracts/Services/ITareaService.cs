using ConciliacDesafio.Domain.Dtos;

namespace ConciliacDesafio.Domain.Contracts.Services
{
    public interface ITareaService
    {
        Task<IEnumerable<TareaDTO>> GetAllTareasAsync();
        Task<TareaDTO> GetTareaByIdAsync(int id);
        Task<TareaDTO> EditarTareaAsync(int id, TareaDTO tareaDTO);
        Task<TareaDTO> CambiarTareaPendienteACompletadaAsync(int id);
        Task<TareaDTO> CrearTareaAsync(TareaDTO tareaDTO);
        Task<TareaDTO> BorrarTareaAsync(int id);
    }
}
