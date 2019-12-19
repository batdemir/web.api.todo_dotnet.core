using System;

namespace web.api.todo.Models.Mapper {

    public interface IViewUser {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}