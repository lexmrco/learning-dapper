using System;
using System.Threading.Tasks;
using Tekus.Datos;
using System.Linq;
using Tekus.Entidades;

namespace Tekus.Servicios.Pruebas
{
    class Program
    {
        static async Task Main(string[] args)
        {
            DataConnection.ConnectionString = "Data Source=localhost;Initial Catalog=db_tekus;User ID=sa;Password=p@ssw0rd";
            // Paginación
            using (var uw = new UnitOfWork())
            {
                var result = await uw.Paises.Find(new Datos.Filters.PaisFilter() { },new Datos.Filters.Pagination(4,3));
                Console.WriteLine($"Total registros: {result.TotalRows} \n Total Páginas: {result.Pages} \n Página solicitada: {result.CurrentPage} \n Filas por página: {result.RowsPerPage} \n Datos devueltos: {result.Data.Count()}");

                foreach (Pais pais in result.Data)
                {
                    Console.WriteLine($"Pais: {pais.Nombre} \n ID: {pais.ID}");
                }
                // Crear cliente
                Cliente cliente1 = new Cliente() { 
                    Nombre = "Claro",
                    CorreoElectronico = "gerente@claro.com",
                    NIT = "47892343"
                };

                // Crear cliente
                Cliente cliente2 = new Cliente()
                {
                    Nombre = "CEMEX",
                    CorreoElectronico = "gerente@cemex.com",
                    NIT = "5384796"
                };
                await uw.Clientes.Create(cliente1);

                await uw.Clientes.Create(cliente2);

                //await uw.Clientes.Reset();

                uw.Commit();
                Console.WriteLine($"Cliente creado");
            }

            Console.ReadKey(true);
        }

        void CreateCliente(UnitOfWork unitOfWork)
        {

        }
    }
}
