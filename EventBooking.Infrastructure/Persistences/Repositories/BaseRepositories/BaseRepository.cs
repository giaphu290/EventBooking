using EventBooking.Application.Common.Persistences.IRepositories.IBaseRepositories;
using EventBooking.Domain.Entities.BaseEntities;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace EventBooking.Infrastructure.Persistences.Repositories.BaseRepositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, IBaseEntity
    {
        private readonly DbContext _dbContext;
        public BaseRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // Một property chỉ đọc trả về một IQueryable<T>, đại diện cho tập hợp các thực thể T trong cơ sở dữ liệu.
          // Một điều kiện lọc LINQ chỉ lấy những thực thể mà thuộc tính IsActive là true và IsDelete là false.
        public IQueryable<T> Entities =>_dbContext.Set<T>().Where(e => e.IsActive && !e.IsDelete);
            //Một phương thức bất đồng bộ (async) để thêm một thực thể T vào cơ sở dữ liệu.
        public async Task<T> AddAsync(T entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
            {
                //Gắn thực thể vào ngữ cảnh theo dõi nếu nó bị tách rời.
                _dbContext.Set<T>().Attach(entity);
            }
            // Thêm thực thể vào cơ sở dữ liệu một cách bất đồng bộ.
            await _dbContext.Set<T>().AddAsync(entity);
            return entity;
        }
        // Phương thức bất đồng bộ để lấy tất cả các thực thể T từ cơ sở dữ liệu.
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            //Chuyển đổi IQueryable<T> thành một danh sách List<T> bất đồng bộ.
            return await Entities.ToListAsync();
        }
        // Trả về IQueryable<T>, cho phép thực hiện các thao tác truy vấn LINQ thêm vào trước khi thực thi.
        public IQueryable<T> GetAllQueryable()
        {
            return Entities;
        }

        public Task<IQueryable<T>> GetAllQueryableAsync()
        {
            return Task.FromResult(Entities);
        }

        public async Task<T> GetByIdAsync(object id)
        {
            //  Tìm entity theo id một cách bất đồng bộ.
            var entity = await _dbContext.Set<T>().FindAsync(id);
            if (entity != null && entity.IsActive && !entity.IsDelete)
            {
                return (entity);
            }
            return null;
        }
        // Một tham số CancellationToken tùy chọn để hủy bỏ thao tác bất đồng bộ nếu cần.
        public async Task<T> GetByIdAsync(object id, CancellationToken cancellationToken = default)
        {
            //Tìm entity theo id một cách bất đồng bộ.
            var entity = await _dbContext.Set<T>().FindAsync(id, cancellationToken);
            // Kiểm tra nếu entity tồn tại và đáp ứng các điều kiện IsActive và IsDelete.
            if (entity != null && entity.IsActive && !entity.IsDelete)
            {
                return entity;
            }
            return null;
        }

        public void Remove(T entity)
        {
            //Loại bỏ entity khỏi ngữ cảnh theo dõi.
            _dbContext.Set<T>().Remove(entity);
        }

        public async Task<List<T>> ToListAsync(IQueryable<T> query, CancellationToken cancellationToken = default)
        {
            //Phương thức bất đồng bộ để chuyển đổi một truy vấn IQueryable<T> thành danh sách List<T>, hỗ trợ hủy tác vụ bằng CancellationToken.
            //Thực thi truy vấn và chuyển đổi kết quả thành List<T> bất đồng bộ.
            return await query.ToListAsync(cancellationToken);
        }
        //Phương thức đồng bộ để cập nhật một thực thể T trong cơ sở dữ liệu
        public void Update(T entity)
        {
            //Đánh dấu thực thể là đã sửa đổi để Entity Framework theo dõi các thay đổi và áp dụng chúng khi SaveChanges được gọi.
            _dbContext.Set<T>().Update(entity);
        }
    }
}
