using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDo.Models
{
    public class TodoContext:DbContext
    {
        //default constructor, expectiong db option passed in
        //Dep. Injection need
        public TodoContext(DbContextOptions<TodoContext> options)
            :base(options)
        {

        }
        
        //deff constructor
        public TodoContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            base.OnConfiguring(options);
            options.UseSqlite("Data Source = ToDo.db");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TodoItems>().HasData(
                new TodoItems() {Id=1, Name = "Task 1"},
                new TodoItems() {Id=2, Name= "Task 2", IsComplete=true},
                new TodoItems() {Id=3, Name="Task3"}
                );
        }

        public DbSet<TodoItems> TodoItems { get; set; }
    }
}
