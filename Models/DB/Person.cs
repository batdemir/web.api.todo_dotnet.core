using System;

namespace web.api.todo.Models.DB {
#pragma warning disable RECS0001 // Class is declared partial but has only one part
    public partial class Person {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
    }
#pragma warning restore RECS0001 // Class is declared partial but has only one part
}
