using AutoMapper;
using ConciliacDesafio.Domain.Dtos;
using ConciliacDesafio.Domain.Entities;

namespace ConciliacDesafio.Persistence.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Tarea, TareaDTO>();
            CreateMap<TareaDTO, Tarea>();
        }
    }
}
