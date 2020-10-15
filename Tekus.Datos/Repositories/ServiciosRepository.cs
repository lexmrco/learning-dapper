using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Tekus.Datos.Filters;
using Tekus.Entidades;

namespace Tekus.Datos.Repositories
{
    public interface IServiciosRepository : IReadRepository<Servicio, ServicioFilter>, IWriteRepository<Servicio>
    {

    }
    public class ServiciosRepository : BaseRepository<Servicio, Servicio, ServicioFilter>, IServiciosRepository
    {
        public ServiciosRepository(IDbTransaction transaction):base(transaction)
        {

        }
        public override string TableName => "Servicios";

        public override string CreateQuery => "INSERT INTO [dbo].[Servicios]([Nombre],[ValorHora],[ClienteID],[PaisID]) VALUES(@Nombre,@ValorHora,@ClienteID,@PaisID)";

        public override string UpdateQuery => "UPDATE [dbo].[Servicios] SET [Nombre] = @Nombre,[ValorHora] = @ValorHora,[ClienteID] = @ClienteID,[PaisID] = @PaisID WHERE [ID] = @ID";
    }
}
