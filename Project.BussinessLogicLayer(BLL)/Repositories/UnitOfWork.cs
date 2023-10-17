using Microsoft.EntityFrameworkCore.ChangeTracking;
using Project.BussinessLogicLayer_BLL_.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BussinessLogicLayer_BLL_.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IDepartmentRepository departmentRepository, IEmployeeRepository employeeRepository)
        {
            DepartmentRepository = departmentRepository;
            EmployeeRepository = employeeRepository;
        }

        public IDepartmentRepository DepartmentRepository { get; set ; }
        public IEmployeeRepository EmployeeRepository { get ; set ; }
    }
}
