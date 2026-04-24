using Microsoft.EntityFrameworkCore;
using EmployeeDirectory.Api.Models;

namespace EmployeeDirectory.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Employee> Employees { get; set; }
}