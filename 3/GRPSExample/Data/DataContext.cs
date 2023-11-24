using GRPSExample.Entities;
using Microsoft.EntityFrameworkCore;

namespace GRPSExample.Data;

public class DataContext:DbContext
{
    public DataContext(DbContextOptions<DataContext> options):base(options)
    {
    }

    public DbSet<ToDo> ToDoS { get; set; }
    
}