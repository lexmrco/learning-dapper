using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Tekus.Entities;

namespace Tekus.Servicios.Datos.Mapping
{
    public class ServicioMap : IEntityTypeConfiguration<Servicio>
    {
        public void Configure(EntityTypeBuilder<Servicio> builder)
        {
            builder.ToTable("Servicios")
                .HasOne(c => c.Cliente).WithMany().HasForeignKey(x => x.ClienteId);
            builder.HasOne(c => c.Pais).WithMany().HasForeignKey(x => x.PaisId);
            InitialData(builder);
        }

        private void InitialData(EntityTypeBuilder<Servicio> builder)
        {
            builder.HasData(new Servicio
            {
                PaisId = 1,
                Nombre = "Servicio 1",
                ClienteId = 1,
            }, new Servicio
            {
                PaisId = 2,
                Nombre = "Servicio 2",
                ClienteId = 1,
            });
        }
    }
}
