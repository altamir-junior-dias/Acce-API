using System;
using System.Collections.Generic;

namespace Acce.Domain
{
    public interface IService<TE>
        where TE : EntityBase
    {
        TE SearchById(long id);
        IEnumerable<TE> SearchByCriteria(dynamic criteria);
        IEnumerable<TE> SearchAll();

        long Insert(TE item, bool forceCommit = true);
        void Update(TE item, bool forceCommit = true);
        void Delete(long id, bool forceCommit = true);

        void Validate(TE item);
    }
}