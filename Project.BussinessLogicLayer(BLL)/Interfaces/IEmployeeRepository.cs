using Project.DataAccessLayer_DAL_.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BussinessLogicLayer_BLL_.Interfaces
{
    public interface IEmployeeRepository: IGenericRepository<Employee>
    {
        //Employee Get(int? id);
        //IEnumerable<Employee> GetAll();
        //int Add(Employee department);
        //int Update(Employee department);
        //int Delete(Employee department);
        Task<IEnumerable<Employee>> GetEmployeesByDepartmentName(string DeptName);
        Task<string> GetDepartmentByEmployeeId(int? id);

        Task<IEnumerable<Employee>> Search(string name );
    }
}
