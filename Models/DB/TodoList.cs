using System;

namespace web.api.todo.Models.DB {

#pragma warning disable RECS0001 // Class is declared partial but has only one part
    public partial class TodoList {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid TodoId { get; set; }
    }
#pragma warning restore RECS0001 // Class is declared partial but has only one part
}
