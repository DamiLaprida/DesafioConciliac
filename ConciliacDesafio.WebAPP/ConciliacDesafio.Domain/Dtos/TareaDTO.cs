using ConciliacDesafio.Domain.Entities;

namespace ConciliacDesafio.Domain.Dtos
{
#nullable disable
    public class TareaDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public Estado Estado { get; set; }
    }
}
