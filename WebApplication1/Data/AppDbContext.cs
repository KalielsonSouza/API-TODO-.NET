using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace MeuTodo.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Todo> Todos { get; set; }
        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
            =>  optionsBuilder.UseSqlite("DataSource=app.db;Cache=shared")
;        
    }
}
