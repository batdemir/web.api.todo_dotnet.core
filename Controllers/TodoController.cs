using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using web.api.todo.DAL;
using web.api.todo.Models.DB;
using web.api.todo.Models.Response;

namespace web.api.todo.Controllers {

    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase {

        private TODOContext context;

        public TodoController(TODOContext context) {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<ResponseModel<List<Todo>>> Get() {
            return new TodoService(context).Get();
        }

        [HttpGet("{id}")]
        public ActionResult<ResponseModel<Todo>> Get(Guid id) {
            return new TodoService(context).GetById(id);
        }

        [HttpPost]
        public ActionResult<ResponseModel<Todo>> Post([FromBody]Todo value) {
            return new TodoService(context).Insert(value);
        }

        [HttpPut("{id}")]
        public ActionResult<ResponseModel<Todo>> Put([FromBody]Todo value) {
            return new TodoService(context).Update(value);
        }

        [HttpDelete("{id}")]
        public ActionResult<ResponseModel<bool>> Delete(Guid id) {
            return new TodoService(context).Delete(id);
        }
    }
}
