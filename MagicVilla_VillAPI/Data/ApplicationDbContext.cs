using MagicVilla_VillAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillAPI.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Villa> Villas { get; set; }
    }
}
