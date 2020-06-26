using System.Collections.Generic;
using System.Data;
using Bootstrapping.Repositories;

namespace Bootstrapping.Wrappers
{
    public interface IConnectionWrapper
    {
        TR Get<TR>(long id, IDbTransaction transaction) where TR : RecordBase;
        IEnumerable<TR> Query<TR>(string query, object filter, IDbTransaction transaction) where TR : RecordBase;
        IEnumerable<TR> GetAll<TR>(IDbTransaction transaction) where TR : RecordBase;
        long Insert<TR>(TR item, IDbTransaction transaction) where TR : RecordBase;
        bool Update<TR>(TR item, IDbTransaction transaction) where TR : RecordBase;
        bool Delete<TR>(TR item, IDbTransaction transaction) where TR : RecordBase;
    }
}