using Microsoft.EntityFrameworkCore;
using Valkimia.Models;

namespace Valkimia.Context
{
    public class ValkimiaContext : DbContext
    {
        private const string Schema = "Valkimia";

        public ValkimiaContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema(Schema);

            builder.Entity<Ciudad>().HasData(
                new Ciudad
                {
                    Id = 1,
                    Nombre = "San Miguel de Tucumán"
                },
                new Ciudad
                {
                    Id = 2,
                    Nombre = "Salta"
                },
                new Ciudad
                {
                    Id = 3,
                    Nombre = "Rosario"
                },
                new Ciudad
                {
                    Id = 4,
                    Nombre = "La Plata"
                },
                new Ciudad
                {
                    Id = 5,
                    Nombre = "La Pampa"
                });
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<Ciudad> Ciudades  { get; set; }

    }
}
