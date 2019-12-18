using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using web.api.todo.DAL;
using web.api.todo.Models;
using web.api.todo.Models.DB;

namespace web.api.todo.Controllers {

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase {

        private TODOContext context;

        public UserController(TODOContext context) {
            this.context = context;
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
    }
}
