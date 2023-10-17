using Microsoft.EntityFrameworkCore;
using Project.BussinessLogicLayer_BLL_.Interfaces;
using Project.DataAccessLayer_DAL_.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BussinessLogicLayer_BLL_.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly MVCAppDbContext context;

        public GenericRepository(MVCAppDbContext context)
        {
            this.context = context;
        }
        public async Task<int> Add(T obj)
        {
            await context.Set<T>().AddAsync(obj);
            return await context.SaveChangesAsync();
        }

        public async Task<int> Delete(T obj)
        {
            context.Set<T>().Remove(obj);
            return await context.SaveChangesAsync();
        }

        public async Task<T> Get(int? id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<int> Update(T obj)
        {
            context.Set<T>().Update(obj);
            return await context.SaveChangesAsync();
        }
    }
}
