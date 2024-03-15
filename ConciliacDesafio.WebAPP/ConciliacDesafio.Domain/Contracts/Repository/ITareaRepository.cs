using ConciliacDesafio.Domain.Dtos;

namespace ConciliacDesafio.Domain.Contracts.Repository
{
    public interface ITareaRepository
    {
        Task<IEnumerable<TareaDTO>> GetAllTareasAsync();
        Task<TareaDTO> GetTareaByIdAsync(int id);
        Task<TareaDTO> EditarTareaAsync(int id, TareaDTO tareaDTO);
        Task<TareaDTO> CambiarTareaPendienteACompletadaAsync(int id);
        Task<TareaDTO> CrearTareaAsync(TareaDTO tareaDto);
        Task<TareaDTO> BorrarTareaAsync(int id);
    }
}
