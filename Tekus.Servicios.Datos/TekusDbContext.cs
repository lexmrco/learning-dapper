using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tekus.Entities;

namespace Tekus.Servicios.Datos
{
    public class TekusDbContext: DbContext
    {
        public TekusDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Servicio> Servicios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new ClienteMap());
            //modelBuilder.ApplyConfiguration(new ServicioMap());

        }
    }
}
