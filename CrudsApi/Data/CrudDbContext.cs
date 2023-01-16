using CrudsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudsApi.Data
{
    public class CrudDbContext : DbContext
    {
        public CrudDbContext( DbContextOptions options) : base(options)
        {

        }

        public DbSet<Employee> Employees{ get; set; }
    }
}
