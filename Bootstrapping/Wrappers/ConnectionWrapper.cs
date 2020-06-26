using System.Collections.Generic;
using System.Data;
using Bootstrapping.Repositories;
using Dapper;
using Dapper.Contrib.Extensions;

namespace Bootstrapping.Wrappers
{
    public class ConnectionWrapper : IConnectionWrapper
    {
        protected IDbConnection connection;

        public ConnectionWrapper(IDbConnection connection) { 
            this.connection = connection;
        }

        public TR Get<TR>(long id, IDbTransaction transaction) where TR : RecordBase {
            return connection.Get<TR>(id, transaction);
        }

        public IEnumerable<TR> Query<TR>(string query, object filter, IDbTransaction transaction) where TR : RecordBase {
            return connection.Query<TR>(query, filter, transaction);
        }

        public IEnumerable<TR> GetAll<TR>(IDbTransaction transaction) where TR : RecordBase {
            return connection.GetAll<TR>(transaction);
        }

        public long Insert<TR>(TR item, IDbTransaction transaction) where TR : RecordBase {
            return connection.Insert(item, transaction);
        }

        public bool Update<TR>(TR item, IDbTransaction transaction) where TR : RecordBase {
            return connection.Update(item, transaction);
        }

        public bool Delete<TR>(TR item, IDbTransaction transaction) where TR : RecordBase {
            return connection.Delete(item, transaction);
        }
    }
}