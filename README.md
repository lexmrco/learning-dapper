# Web API Dapper
Web API FULL REST .Net Core. 
Aplicación base para operaciones crud en base de datos, permite paginación en la consulta.

Uso de :
* .Net Core
* SQL Server
* Dapper
* AutoMapper
* Capas. Datos, entidades, servicios web.

## Scafolding
* Datos: Repositorio, uso de dapper para consultas a la base de datos
* Entidades: Objetos de base de datos
* REST: Controladores web api
  * DbScripts: Script para crear base de datos

## Enpoints
### Listar clientes
* Url: http://localhost:61193/api/clientes
* Método: GET
* params:
  * rowsPerPage: Filas por página para la consulta
  * page: Página de datos que se quiere ver
* Response: OK. Lista de clientes

### Crear un nuevo cliente
* Url: http://localhost:61193/api/clientes
* Método: POST
* Body:
  * JSON Data: Datos a insertar
   * NIT: Número de identificación del cliente
   * Nombre: Nombre del cliente
   * CorreoElectronico: Correo electrónico del cliente
 * Response: OK. Datos insertados
 
 ### Actualizar cliente
 
 * Url: http://localhost:61193/api/clientes?id=[ide de cliente]
* Método: PUT
* Body:
  * JSON Data: Datos a insertar
   * NIT: Número de identificación del cliente
   * Nombre: Nombre del cliente
   * CorreoElectronico: Correo electrónico del cliente
* Response: OK. Datos Actualizados
  
### Actualizar cliente
 
 * Url: http://localhost:61193/api/clientes?id=[ide de cliente]
* Método: DELETE
* Response: OK. Datos Eliminados
