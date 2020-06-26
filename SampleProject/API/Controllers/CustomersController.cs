using System;
using API.Entities;
using AutoMapper;
using Bootstrapping.Controllers;
using Bootstrapping.Domain;
using Domain.Entities;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("customers")]
    public class CustomersController : BaseController
    {
        private readonly ICustomersService service;
        private readonly IMapper mapper;

        public CustomersController(ICustomersService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        [HttpGet, Route("")]
        public ActionResult GetAll() 
        {
            Func<dynamic> process = () => { return service.SearchAll(); };

            return ExecuteProcess(process, "customer");
        }

        [HttpPost, Route("")]
        public ActionResult Add([FromBody] CustomerDTO customer)
        {
            Func<long> process = () => { return service.Insert(mapper.Map<Customer>(customer)); };

            return ExecuteProcess(process, "customer");
        }

        [HttpPut, Route("{id}")]
        public ActionResult Update(long id, [FromBody] CustomerDTO customer)
        {
            Action process = () => { service.Update(mapper.Map<Customer>(customer, opt => opt.AfterMap((_, entity) => ((EntityBase)entity).Id = id))); };

            return ExecuteProcess(process, "customer");
        }

        [HttpDelete, Route("{id}")]
        public ActionResult Delete(long id)
        {
            Action process = () => { service.Delete(id); };

            return ExecuteProcess(process, "customer");
        }

        [HttpGet, Route("{id}")]
        public ActionResult Get(long id) 
        {
            Func<dynamic> process = () => { return service.SearchById(id); };

            return ExecuteProcess(process, "customer");
        }
    }
}