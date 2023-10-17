using Project.DataAccessLayer_DAL_.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BussinessLogicLayer_BLL_.Interfaces
{
    public interface IGenericRepository<T>
    {
        Task<T> Get(int? id);
        Task<IEnumerable<T>> GetAll();
        Task<int> Add(T obj);
        Task<int> Update(T obj);
        Task<int> Delete(T obj);
    }
}
