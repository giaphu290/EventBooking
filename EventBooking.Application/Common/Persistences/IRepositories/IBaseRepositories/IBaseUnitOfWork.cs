using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBooking.Application.Common.Persistences.IRepositories.IBaseRepositories
{
    public interface IBaseUnitOfWork
    {
        Task<int> SaveChangeAsync();
        void Dispose();
    }
}
