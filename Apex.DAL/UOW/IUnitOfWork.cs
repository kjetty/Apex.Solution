using Apex.DAL.Repositories;
using System;

namespace Apex.DAL.UOW
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }

        void SaveDBChanges();
    }
}
