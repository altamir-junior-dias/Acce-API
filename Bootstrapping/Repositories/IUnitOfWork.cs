using System;
using System.Data;
using Bootstrapping.Wrappers;

namespace Bootstrapping.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IDbTransaction CurrentTransaction { get; }
        IConnectionWrapper Connection { get; }

        void Commit();
    }
}
