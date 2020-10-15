using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Tekus.Entities;

namespace Tekus.Servicios.Datos.Mapping
{
    public class ClienteMap : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Clientes")
                .HasMany(c => c.Servicios).WithOne(x => x.Cliente).HasForeignKey(x => x.ID);
            InitialData(builder);
        }

        private void InitialData(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasData(new Cliente
            {
                NIT = "789432",
                Nombre = "Stephen",
                CorreoElectronico = "Strange",
            }, new Cliente
            {
                NIT = "789432",
                Nombre = "Stephen",
                CorreoElectronico = "Strange",
            });
        }
    }
}
