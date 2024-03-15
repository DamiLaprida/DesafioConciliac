using AutoMapper;
using ConciliacDesafio.Domain.Contracts.Repository;
using ConciliacDesafio.Domain.Dtos;
using ConciliacDesafio.Domain.Entities;
using ConciliacDesafio.Persistence.Context;
using Microsoft.EntityFrameworkCore;

#nullable disable
namespace ConciliacDesafio.Persistence.Repositories
{
    public class TareaRepository : ITareaRepository
    {
        private readonly TareasContext _tareasContext;
        private readonly IMapper _mapper;
        public TareaRepository(TareasContext tareasContext, IMapper mapper)
        {
            _tareasContext = tareasContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TareaDTO>> GetAllTareasAsync()
        {
            var tareas = _mapper.Map<IEnumerable<TareaDTO>>(await _tareasContext.Tareas.ToListAsync());
            return tareas;
        }

        public async Task<TareaDTO> GetTareaByIdAsync(int id)
        {
            var tarea = await _tareasContext.Tareas.Where(tareaDb => tareaDb.Id == id).FirstOrDefaultAsync();
            if (tarea is null)
                throw new NullReferenceException($"No existe ninguna tarea con id: {id}.");

            return _mapper.Map<TareaDTO>(tarea);
        }

        public async Task<TareaDTO> EditarTareaAsync(int id, TareaDTO tareaDTO)
        {
            var tarea = await _tareasContext.Tareas.FindAsync(id);
            if (tarea == null)
            {
                throw new NullReferenceException($"No existe ninguna tarea con id: {id}.");
            }

            if (tareaDTO.Titulo != null)
            {
                tarea.Titulo = tareaDTO.Titulo;
            }

            if (tareaDTO.Descripcion != null)
            {
                tarea.Descripcion = tareaDTO.Descripcion;
            }

            await _tareasContext.SaveChangesAsync();
            return _mapper.Map<TareaDTO>(tarea);
        }

        public async Task<TareaDTO> CambiarTareaPendienteACompletadaAsync(int id)
        {
            var tarea =  await _tareasContext.Tareas.Where(tareaDb => tareaDb.Id == id).FirstOrDefaultAsync();
            if (tarea is null)
                throw new NullReferenceException($"No existe ninguna tarea con id: {id}");

            if(tarea.Estado == Estado.Pendiente)
                tarea.Estado = Estado.Completada;

             await _tareasContext.SaveChangesAsync();

            var tareaDTO = _mapper.Map<TareaDTO>(tarea);
            return tareaDTO;
        }

        public async Task<TareaDTO> CrearTareaAsync(TareaDTO tareaDto)
        {
            var tarea = _mapper.Map<Tarea>(tareaDto);
            _tareasContext.Add(tarea);
            await _tareasContext.SaveChangesAsync();

            return tareaDto;
        }

        public async Task<TareaDTO> BorrarTareaAsync(int id)
        {
            var tarea = await _tareasContext.Tareas.Where(tareaDb => tareaDb.Id == id).FirstOrDefaultAsync();
            if (tarea is null)
                throw new NullReferenceException($"No existe ninguna tarea con id: {id}");

            _tareasContext.Remove(tarea);
            await _tareasContext.SaveChangesAsync();
            var tareaDTO = _mapper.Map<TareaDTO>(tarea);
            return tareaDTO;
        }
    }
}
