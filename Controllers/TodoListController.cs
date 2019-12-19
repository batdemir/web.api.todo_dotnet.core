using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using web.api.todo.DAL;
using web.api.todo.Models;
using web.api.todo.Models.DB;
using web.api.todo.Models.Response;

namespace web.api.todo.Controllers {

    [ApiController]
    [Route("[controller]")]
    public class TodoListController :ControllerBase {

        private TODOContext context;

        public TodoListController(TODOContext context) {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<ResponseModel<List<CustomTodoList>>> Get() {
            return new TodoListService(context).Get();
        }

        [HttpGet("todo/{id}")]
        public ActionResult<ResponseModel<List<CustomTodoList>>> GetByTodo(Guid todoId) {
            return new TodoListService(context).GetByTodo(todoId);
        }

        [HttpGet("user/{id}")]
        public ActionResult<ResponseModel<List<CustomTodoList>>> GetByUser(Guid userId) {
            return new TodoListService(context).GetByUser(userId);
        }
    }
}
