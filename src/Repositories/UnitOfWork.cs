using System.Data;
using Acce.Wrappers;

namespace Acce.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private IDbConnection connection;

		private IDbTransaction transaction;

		private IGarbageCollectorWrapper garbageCollectorWrapper;

		private bool disposed;

		public UnitOfWork(IDbConnection connection, IGarbageCollectorWrapper garbageCollectorWrapper)
		{
			this.connection = connection;
			this.connection.Open();

			transaction = this.connection.BeginTransaction();

			this.garbageCollectorWrapper = garbageCollectorWrapper;
		}

		public IDbTransaction CurrentTransaction { get { return transaction; } }
		public IConnectionWrapper Connection { get { return new ConnectionWrapper(transaction.Connection); } }

		public void Commit()
		{
			try
			{
				transaction.Commit();
			}
			catch
			{
				transaction.Rollback();
				throw;
			}
			finally
			{
				transaction.Dispose();
				transaction = connection.BeginTransaction();
			}
		}

		public void Dispose()
		{
			doDispose(true);

			garbageCollectorWrapper.SuppressFinalize(this);
		}

		private void doDispose(bool disposing)
		{
			if (!disposed)
			{
				if (disposing)
				{
					if (transaction != null)
					{
						transaction.Dispose();
						transaction = null;
					}
					if (connection != null)
					{
						connection.Dispose();
						connection = null;
					}
				}
				disposed = true;
			}
		}

		~UnitOfWork()
		{
			doDispose(false);
		}
	}
}
