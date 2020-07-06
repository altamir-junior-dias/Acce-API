using System.Collections.Generic;

namespace Acce.Repositories
{
    public interface IRepository<TR> where TR : RecordBase
    {
        void SetUnitOfWork(IUnitOfWork unitOfWork);

        TR SearchById(long id);
        IEnumerable<TR> SearchByCriteria(dynamic filter);

        IEnumerable<TR> SearchAll();

        long Insert(TR item);
        bool Update(TR item);
        bool Delete(long id);
    }
}
