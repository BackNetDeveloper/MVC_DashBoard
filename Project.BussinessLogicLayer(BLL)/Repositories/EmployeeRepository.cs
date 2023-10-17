using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
    public class EmployeeRepository : GenericRepository<Employee>,IEmployeeRepository
    {
        private readonly MVCAppDbContext context;

        public EmployeeRepository(MVCAppDbContext context):base(context)
        {
            this.context = context;
        }

        public async Task<string> GetDepartmentByEmployeeId(int? id)
        {
            var employee = await context.Employees.Where(E => E.Id == id).Include(E=>E.Department).FirstOrDefaultAsync();
            var DepartmentName = employee.Department.Name;
            return DepartmentName;
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByDepartmentName(string DeptName)
        => await context.Employees.Where(E => E.Department.Name == DeptName).ToListAsync();

        public async Task<IEnumerable<Employee>> Search(string name)
         => await context.Employees.Where(E => E.Name.Contains(name)).ToListAsync();


        ////public int Add(Employee employee)
        ////{
        ////    context.Employees.Add(employee);
        ////    return context.SaveChanges();
        ////}

        ////public int Delete(Employee employee)
        ////{
        ////    context.Employees.Remove(employee);
        ////    return context.SaveChanges();
        ////}

        ////public IEnumerable<Employee> GetAll()
        ////=> context.Employees.ToList();

        ////public Employee Get(int? id)
        ////    => context.Employees.FirstOrDefault(E => E.Id == id);

        ////public int Update(Employee employee)
        ////{
        ////    context.Employees.Update(employee);
        ////    return context.SaveChanges();
        ////}
    }
}
