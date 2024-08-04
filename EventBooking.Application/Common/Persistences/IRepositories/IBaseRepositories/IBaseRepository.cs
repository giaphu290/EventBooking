using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Common.Persistences.IRepositories.IBaseRepositories
{
    public interface IBaseRepository<T> where T : class
    {
        IQueryable<T> Entities { get; }
        IQueryable<T> GetAllQueryable();
        Task<IQueryable<T>> GetAllQueryableAsync();
        Task<List<T>> ToListAsync(IQueryable<T> query, CancellationToken cancellationToken = default);
        Task<T> GetByIdAsync(object id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> AddAsync(T entity);
        void Remove(T entity);
        void Update(T entity);

    }
}
