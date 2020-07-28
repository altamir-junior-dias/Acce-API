using Acce.Wrappers;
using Dapper.Contrib.Extensions;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Acce.Repositories
{
    public abstract class RepositoryBase<TR> : IRepository<TR> 
        where TR : RecordBase, new()
    {
        private IUnitOfWork unitOfWork;

        public virtual void SetUnitOfWork(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        protected IConnectionWrapper Connection { get { return unitOfWork.Connection; } }

        protected IDbTransaction Transaction { get { return unitOfWork.CurrentTransaction; } }

        public virtual TR SearchById(long id)
        {
            return Connection.Get<TR>(id, Transaction);
        }

        public IEnumerable<TR> SearchByCriteria(dynamic filter)
        {
            var table = typeof(TR).GetCustomAttributes(typeof(TableAttribute), true).FirstOrDefault() as TableAttribute;

            var parameters = ((object)filter).GetType().GetProperties().Select(parameter => parameter.Name);

            var query = "SELECT * FROM " + table.Name + " WHERE ";
            query += string.Join(" and ", parameters.Select(parameter => parameter + " = @" + parameter));

            return Connection.Query<TR>(query, (object)filter, Transaction);
        }

        public virtual IEnumerable<TR> SearchAll()
        {
            return Connection.GetAll<TR>(Transaction);
        }

        public long Insert(TR item)
        {
            return Connection.Insert<TR>(item, Transaction);
        }

        public bool Update(TR item)
        {
            return Connection.Update<TR>(item, Transaction);
        }

        public bool Delete(long id)
        {
            TR record = new TR();

            var primaryKeyProperty = typeof(TR).GetProperties().Where(p => p.GetCustomAttributes(typeof(KeyAttribute), true).FirstOrDefault() != null).FirstOrDefault();

            if (primaryKeyProperty != null) primaryKeyProperty.SetValue(record, id);

            return Connection.Delete<TR>(record, Transaction);
        }
    }
}