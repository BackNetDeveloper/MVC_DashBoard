using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project.DataAccessLayer_DAL_.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DataAccessLayer_DAL_.Contexts
{
    public class MVCAppDbContext : IdentityDbContext<ApplicationUser>
    {
        public MVCAppDbContext(DbContextOptions<MVCAppDbContext> options) : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //=> optionsBuilder.UseSqlServer("server = . ; database = MVCAppDb ; integrated security = True");

        public DbSet<Department> Departments {get;set;}
        public DbSet<Employee> Employees {get;set;}
    }
}
