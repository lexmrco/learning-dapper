using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Tekus.Entities;

namespace Tekus.Servicios.Datos.Mapping
{
    public class PaisMap : IEntityTypeConfiguration<Pais>
    {
        public void Configure(EntityTypeBuilder<Pais> builder)
        {
            builder.ToTable("Paises");
            InitialData(builder);
        }

        private void InitialData(EntityTypeBuilder<Pais> builder)
        {
            builder.HasData(new Pais
            {
                Nombre = "Pais 1",
            }, new Pais
            {
                Nombre = "Pais 2"
            });
        }
    }
}
