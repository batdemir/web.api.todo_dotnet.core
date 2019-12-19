using System;

namespace web.api.todo.Models.Mapper {

    public class ViewUser :IViewUser {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CustomName { get; set; }
    }
}