using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Tekus.Datos;
using Tekus.Datos.Filters;
using Tekus.Datos.Repositories;
using Tekus.Entidades;
using Tekus.REST.Models;

namespace Tekus.REST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        readonly MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Cliente, ClienteModel>();
            cfg.CreateMap<ClienteModel, Cliente>();
        });
        readonly IMapper iMapper;

        public ClientesController()
        {
            iMapper = config.CreateMapper();
        }
        

        // GET: api/Locations
        [HttpGet]
        public async Task<QueryResult> Get(int? page, int? rowsPerPage)
        {
            page ??= 1;
            rowsPerPage ??= 10;
            using (var uw = new UnitOfWork())
            {
                var result = await uw.Clientes.Find(new Pagination(page.Value, rowsPerPage.Value));
                return result;
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(ClienteModel model)
        {
            string errorMessage;
            if (!this.ModelState.IsValid)
            {
                errorMessage = GetModelStateMessages(this.ModelState);
                return BadRequest(errorMessage);
            }
            
            var entidad = iMapper.Map<ClienteModel, Cliente>(model);
            using (var uw = new UnitOfWork())
            {
                await uw.Clientes.Create(entidad);
                uw.Commit();
            }
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id, ClienteModel model)
        {
            string errorMessage;
            if (!this.ModelState.IsValid)
            {
                errorMessage = GetModelStateMessages(this.ModelState);
                return BadRequest(errorMessage);
            }

            using (var uw = new UnitOfWork())
            {
                var entidad = await uw.Clientes.GetWriteModel(id);
                if (entidad == null)
                    return BadRequest("No se encontró la entidad a actualizar");
                entidad = iMapper.Map<ClienteModel, Cliente>(model);
                entidad.ID = id;
                await uw.Clientes.Update(entidad);
                uw.Commit();
            }
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            string errorMessage;
            if (!this.ModelState.IsValid)
            {
                errorMessage = GetModelStateMessages(this.ModelState);
                return BadRequest(errorMessage);
            }

            using (var uw = new UnitOfWork())
            {
                await uw.Clientes.Delete(id);
                uw.Commit();
            }
            return Ok();
        }
        protected string GetModelStateMessages(ModelStateDictionary modelState)
        {
            string messages = string.Empty;
            foreach (var item in modelState)
            {
                foreach (var error in item.Value.Errors)
                {
                    messages += error.ErrorMessage;
                }
            }
            return messages;
        }
    }
}