using Project.BussinessLogicLayer_BLL_.Interfaces;
using Project.DataAccessLayer_DAL_.Contexts;
using Project.DataAccessLayer_DAL_.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BussinessLogicLayer_BLL_.Repositories
{
    public class DepartmentRepository : GenericRepository<Department> , IDepartmentRepository
    {
        private readonly MVCAppDbContext context;

        public DepartmentRepository(MVCAppDbContext context):base(context)
        {
            this.context = context;
        }

        ////public int Add(Department department)
        ////{
        ////   context.Departments.Add(department);
        ////    return context.SaveChanges();
        ////}

        ////public int Delete(Department department)
        ////{
        ////    context.Departments.Remove(department);
        ////    return context.SaveChanges();
        ////}

        ////public IEnumerable<Department> GetAll()
        ////=> context.Departments.ToList();

        ////public Department Get(int? id)
        ////    => context.Departments.FirstOrDefault(D => D.Id == id);
        
        ////public int Update(Department department)
        ////{
        ////    context.Departments.Update(department);
        ////    return context.SaveChanges();
        ////}
    }
}
