using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LogicLoopTask.Models
{
    public class TaskContext:DbContext
    {
        public TaskContext(DbContextOptions contextOptions):base(contextOptions)
        {
            
        }
        public DbSet<Task> tasks { get; set; }
    }
}
