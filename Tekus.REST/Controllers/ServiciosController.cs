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
    public class ServiciosController : ControllerBase
    {
        // GET: api/Locations
        [HttpGet]
        public async Task<QueryResult> Get(int? page, int? rowsPerPage)
        {
            page ??= 1;
            rowsPerPage ??= 10;
            using (var uw = new UnitOfWork())
            {
                var result = await uw.Servicios.Find(new Pagination(page.Value, rowsPerPage.Value));
                return result;
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(ServicioModel model)
        {
            string errorMessage;
            if (!this.ModelState.IsValid)
            {
                errorMessage = GetModelStateMessages(this.ModelState);
                return BadRequest(errorMessage);
            }
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Servicio, ServicioModel>();
            });

            IMapper iMapper = config.CreateMapper();
            var entidad = iMapper.Map<ServicioModel, Servicio>(model);
            using (var uw = new UnitOfWork())
            {
                await uw.Servicios.Create(entidad);
                uw.Commit();
            }
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id, ServicioModel model)
        {
            string errorMessage;
            if (!this.ModelState.IsValid)
            {
                errorMessage = GetModelStateMessages(this.ModelState);
                return BadRequest(errorMessage);
            }

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Servicio, ServicioModel>();
            });

            IMapper iMapper = config.CreateMapper();
            using (var uw = new UnitOfWork())
            {
                var entidad = await uw.Servicios.GetWriteModel(id);
                entidad = iMapper.Map<ServicioModel, Servicio>(model);
                await uw.Servicios.Update(entidad);
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
                await uw.Servicios.Delete(id);
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