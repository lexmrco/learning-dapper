using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Tekus.Datos.Filters;
using Tekus.Entidades;

namespace Tekus.Datos.Repositories
{
    public interface IClientesRepository : IReadRepository<Cliente, ClienteFilter>, IWriteRepository<Cliente>
    {

    }
    public class ClientesRepository : BaseRepository<Cliente, Cliente, ClienteFilter>, IClientesRepository
    {
        public ClientesRepository(IDbTransaction transaction) : base(transaction)
        {

        }
        public override string TableName => "Clientes";

        public override string CreateQuery => "INSERT INTO [dbo].[Clientes]([NIT],[Nombre],[CorreoElectronico]) VALUES(@NIT, @Nombre, @CorreoElectronico)";

        public override string UpdateQuery => "UPDATE [dbo].[Clientes] SET [NIT] = @NIT,[Nombre] = @Nombre,[CorreoElectronico] = @CorreoElectronico WHERE ID = @ID";
    }
}
