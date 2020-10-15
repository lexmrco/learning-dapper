using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Tekus.Datos.Filters;
using Tekus.Entidades;

namespace Tekus.Datos.Repositories
{
    public interface IPaisesRepository : IReadRepository<Pais, PaisFilter>, IWriteRepository<Pais>
    {

    }
    public class PaisesRepository : BaseRepository<Pais, Pais, PaisFilter>, IPaisesRepository
    {
        public PaisesRepository(IDbTransaction transaction):base(transaction)
        {

        }
        public override string TableName => "Paises";

        public override string CreateQuery => "INSERT INTO [dbo].[Servicios]([Nombre],[ValorHora],[ClienteID],[PaisID]) VALUES(@Nombre,@ValorHora,@ClienteID,@PaisID)";

        public override string UpdateQuery => "UPDATE [dbo].[Servicios] SET [Nombre] = @Nombre,[ValorHora] = @ValorHora,[ClienteID] = @ClienteID,[PaisID] = @PaisID WHERE [ID] = @ID";
    }
}
