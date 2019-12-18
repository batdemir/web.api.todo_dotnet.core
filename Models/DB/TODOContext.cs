using Microsoft.EntityFrameworkCore;

namespace web.api.todo.Models.DB {

#pragma warning disable RECS0001 // Class is declared partial but has only one part
    public partial class TODOContext : DbContext {
        public TODOContext() {
        }

        public TODOContext(DbContextOptions<TODOContext> options)
            : base(options) {
        }

        public virtual DbSet<Todo> Todo { get; set; }
        public virtual DbSet<TodoList> TodoList { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
                optionsBuilder.UseSqlServer("Server = 192.168.1.20;Database=TODO;User=sa;Password=Aura1992");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Todo>(entity => {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TodoList>(entity => {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<User>(entity => {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
#pragma warning restore RECS0001 // Class is declared partial but has only one part
}
