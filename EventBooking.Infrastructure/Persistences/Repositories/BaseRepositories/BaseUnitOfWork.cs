using EventBooking.Application.Common.Persistences.IRepositories.IBaseRepositories;
using EventBooking.Infrastructure.Persistences.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Infrastructure.Persistences.Repositories.BaseRepositories
{
    public class BaseUnitOfWork : IBaseUnitOfWork
    {
        // Constructor của BaseUnitOfWork,
        // nhận một đối tượng ApplicationDbContext làm tham số và gán nó cho _context.
        // Điều này cho phép BaseUnitOfWork có thể sử dụng _context
        // để thực hiện các thao tác với cơ sở dữ liệu.
        private readonly ApplicationDbContext _dbContext;
        public BaseUnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        //Phương thức Dispose thực hiện interface IDisposable,
        //dùng để giải phóng các tài nguyên không được quản lý (unmanaged resources)
        //khi không cần sử dụng nữa
        // Gọi phương thức Dispose trên _context để giải phóng tài nguyên mà DbContext đang sử dụng, như kết nối cơ sở dữ liệu.
        // Điều này rất quan trọng để tránh rò rỉ tài nguyên (memory leaks) trong các ứng dụng dài hạn.
        public void Dispose()
        {
            _dbContext.Dispose();
        }
        // Phương thức bất đồng bộ (async) để lưu tất cả các thay đổi đã thực hiện trên ngữ cảnh (DbContext) vào cơ sở dữ liệu.
        public async Task<int> SaveChangeAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
