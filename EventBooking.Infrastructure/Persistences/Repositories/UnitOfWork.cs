using EventBooking.Application.Common.Persistences.IRepositories;
using EventBooking.Application.Common.Persistences.IRepositories.IBaseRepositories;
using EventBooking.Domain.Entities.BaseEntities;
using EventBooking.Infrastructure.Persistences.DBContext;
using EventBooking.Infrastructure.Persistences.Repositories.BaseRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Infrastructure.Persistences.Repositories
{
    public class UnitOfWork : BaseUnitOfWork, IUnitOfWork
    {
        //Khai báo một trường (field) chỉ đọc (readonly) _dbContext kiểu ApplicationDbContext,
        //dùng để tương tác với cơ sở dữ liệu.
        private readonly ApplicationDbContext _dbContext;

        public UnitOfWork(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        // Khai báo Repository
        //Chúng lưu trữ các đối tượng repository tương ứng, nhưng không được khởi tạo ngay lập tức. 
        //Thay vào đó, chúng được khởi tạo khi cần sử dụng lần đầu.
        private IEventInvitationRepository _eventInvitationRepository;

        private IEventPostRepository _eventPostRepository;
        private IEventRepository _eventRepository;

        private IEventTicketRepository _eventTicketRepository;

        private IGroupRepository _groupRepository;

        private IGroupUserRepository _groupUserRepository;

        private IUserRepository _userRepository;
        private IAllowedEventGroupRepository _allowedEventGroupRepository;
        // ??= là một cú pháp mới trong C# 8.0, được gọi là null-coalescing assignment. 
        //  Nó kiểm tra xem _eventInvitationRepository có phải là null không, nếu phải,
        //  thì khởi tạo một đối tượng mới và gán cho _eventInvitationRepository.
        //  Điều này đảm bảo rằng repository chỉ được khởi tạo khi cần thiết,
        //  tức là khi nó được sử dụng lần đầu tiên (còn được gọi là lazy initialization).
        public IEventInvitationRepository EventInvitationRepository => _eventInvitationRepository ??= new EventInvitationRepository(_dbContext);

        public  IEventPostRepository EventPostRepository => _eventPostRepository ??= new EventPostRepository(_dbContext);

        public  IEventRepository EventRepository => _eventRepository ??= new EventRepository(_dbContext);

        public  IEventTicketRepository EventTicketRepository => _eventTicketRepository ??= new EventTicketRepository(_dbContext);

        public  IGroupRepository GroupRepository => _groupRepository ??= new GroupRepository(_dbContext);

        public  IGroupUserRepository GroupUserRepository => _groupUserRepository ??= new GroupUserRepository(_dbContext);

        public IUserRepository UserRepository => _userRepository ??= new UserRepository(_dbContext);

        public IAllowedEventGroupRepository AllowedEventGroupRepository => _allowedEventGroupRepository ??= new AllowedEventGroupRepository(_dbContext);

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
        public async Task SaveChangeAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public  IBaseRepository<T> GetRepository<T>() where T : class ,IBaseEntity
        {
            return new BaseRepository<T>(_dbContext);
                
        }
    }
}
