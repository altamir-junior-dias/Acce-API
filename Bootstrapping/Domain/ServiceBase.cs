using AutoMapper;
using Bootstrapping.Exceptions;
using Bootstrapping.Repositories;
using System.Collections.Generic;

namespace Bootstrapping.Domain
{
    public abstract class ServiceBase<TE, TR, TIR> : IService<TE> 
        where TE : EntityBase 
        where TR : RecordBase
        where TIR : IRepository<TR>
    {
        protected readonly TIR repository;
        protected readonly IUnitOfWork unitOfWork;
        protected readonly IMapper mapper;

        protected ServiceBase(TIR repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;

            this.repository = repository;
            this.repository.SetUnitOfWork(unitOfWork);

            this.mapper = mapper;
        }

        public virtual TE SearchById(long id)
        {
            var record = mapper.Map<TE>(repository.SearchById(id));

            if (record == null) throw new ItemNotFoundException(typeof(TE).Name);

            return record;
        }

        public IEnumerable<TE> SearchByCriteria(dynamic criteria)
        {
            return mapper.Map<IEnumerable<TE>>(repository.SearchByCriteria(criteria));
        }

        public IEnumerable<TE> SearchAll()
        {
            return mapper.Map<IEnumerable<TE>>(repository.SearchAll());
        }

        public virtual long Insert(TE item, bool forceCommit = true)
        {
            Validate(item);

            var newId = repository.Insert(mapper.Map<TR>(item));
            if (forceCommit) unitOfWork.Commit();

            return newId;
        }

        public virtual void Update(TE item, bool forceCommit = true)
        {
            if (item.Id == 0) throw new PropertyNotProvidedException("Id");
            Validate(item);

            var updated = repository.Update(mapper.Map<TR>(item));
            if (!updated) throw new ItemNotFoundException(typeof(TE).Name);
            if (forceCommit) unitOfWork.Commit();

            return;
        }

        public virtual void Delete(long id, bool forceCommit = true)
        {
            if (id == 0) throw new PropertyNotProvidedException("Id");

            var deleted = repository.Delete(id);
            if (!deleted) throw new ItemNotFoundException(typeof(TE).Name);
            if (forceCommit) unitOfWork.Commit();

            return;
        }

        public virtual void Validate(TE item)
        {            
        }
    }
}
