using System;
using web.api.todo.Models.DB;

namespace web.api.todo.Models {

    public class CustomTodoList :TodoList {

        public string UserName { get; set; }

        public String TodoName { get; set; }
    }
}

