using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournaments.Core.Contracts
{
    public interface IRepositoryBase<T>
    {
        Task<IEnumerable<T>> GetAllAsync(int pageNumber, int pageSize);
        Task<T> GetByIdAsync(int id);
        Task<int> CountAsync();
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
