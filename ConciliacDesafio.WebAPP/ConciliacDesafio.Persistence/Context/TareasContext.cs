using ConciliacDesafio.Domain.Entities;
using Microsoft.EntityFrameworkCore;

#nullable disable
namespace ConciliacDesafio.Persistence.Context
{
    public class TareasContext : DbContext
    {
        public TareasContext()
        {
        }

        public TareasContext(DbContextOptions<TareasContext> options)
            :base(options)
        {
        }

        public DbSet<Tarea> Tareas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tarea>()
                .Property(t => t.Id)
                .ValueGeneratedOnAdd(); //Acá le digo que en la base de datos 'Id' sea autoincremental.
        }
    }
}
