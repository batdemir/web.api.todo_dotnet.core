using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using web.api.todo.BLL;
using web.api.todo.DAL;
using web.api.todo.Models.DB;
using web.api.todo.Models.Mapper;
using web.api.todo.Models.Response;

namespace web.api.todo.Controllers {

    [ApiController]
    [Route("[controller]")]
    public class UserController :ControllerBase {

        private TODOContext context;
        private readonly IMapper mapper;

        public UserController(TODOContext context, IMapper mapper) {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<ResponseModel<List<Person>>> Get() {
            return new UserService(context).Get();
        }

        [HttpGet("{id}")]
        public ActionResult<ResponseModel<Person>> Get(Guid id) {
            return new UserService(context).GetById(id);
        }

        [HttpPost]
        public ActionResult<ResponseModel<Person>> Post([FromBody]Person value) {
            return new UserService(context).Insert(value);
        }

        [HttpPut("{id}")]
        public ActionResult<ResponseModel<Person>> Put([FromBody]Person value) {
            return new UserService(context).Update(value);
        }

        [HttpDelete("{id}")]
        public ActionResult<ResponseModel<bool>> Delete(Guid id) {
            return new UserService(context).Delete(id);
        }

        //Testing mapper
        [HttpGet("mapper/{id}")]
        public ActionResult<ViewUser> GetMapper(Guid id) {
            var data = context.Person.Where(person => person.Id.Equals(id)).FirstOrDefault();
            var result = mapper.Map<ViewUser>(data);
            return result;
        }

        [HttpPut("mapper")]
        public void UpdatetMapper([FromBody] ViewUser model) {
            Person desData = context.Person.Where(q => q.Id.Equals(model.Id)).FirstOrDefault();
            Person data = mapper.Map<Person>(model);

            data.IsActive = desData.IsActive;

            new BLLUser(context).Update(data);
        }
    }
}
