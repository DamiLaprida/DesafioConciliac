using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ConciliacDesafio.Domain.Entities
{
    public enum Estado
    {
        [EnumMember(Value = "Pendiente")]
        Pendiente,

        [EnumMember(Value = "Completada")]
        Completada
    }

    #nullable disable
    public class Tarea
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        [Display(Name = "Estado")]
        [EnumDataType(typeof(Estado))]
        public Estado Estado { get; set; }
    }
}
