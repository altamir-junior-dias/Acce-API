using System;
using System.Data;
using Acce.Wrappers;

namespace Acce.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IDbTransaction CurrentTransaction { get; }
        IConnectionWrapper Connection { get; }

        void Commit();
    }
}
