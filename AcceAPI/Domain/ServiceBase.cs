using AutoMapper;
using Acce.Exceptions;
using Acce.Repositories;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace Acce.Domain
{
    public abstract class ServiceBase<TE, TR, TIR> : IService<TE> 
        where TE : EntityBase 
        where TR : RecordBase
        where TIR : IRepository<TR>
    {
        private IList<ValidationIssue> validationIssues;

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
            if (item.Id == 0) throw new ValidationIssueException(new ValidationIssue { IssueType = IssueTypeEnum.PropertyNotInformed, PropertyName = "Id" });
            Validate(item);

            var updated = repository.Update(mapper.Map<TR>(item));
            if (!updated) throw new ItemNotFoundException(typeof(TE).Name);
            if (forceCommit) unitOfWork.Commit();

            return;
        }

        public virtual void Delete(long id, bool forceCommit = true)
        {
            if (id == 0) throw new ValidationIssueException(new ValidationIssue { IssueType = IssueTypeEnum.PropertyNotInformed, PropertyName = "Id" });

            var deleted = repository.Delete(id);
            if (!deleted) throw new ItemNotFoundException(typeof(TE).Name);
            if (forceCommit) unitOfWork.Commit();

            return;
        }

        public void AddPropertyNotInformedIssue<T>(Expression<Func<T, object>> expression) where T : EntityBase
        {
            var member = expression.Body as MemberExpression;
            if (member == null) member = (expression.Body as UnaryExpression)?.Operand as MemberExpression;

            validationIssues.Add(new ValidationIssue { PropertyName = member.Member.Name });
        }

        public void AddCustomIssue(string message)
        {
            validationIssues.Add(new ValidationIssue { Message = message });
        }

        public virtual void Validating(TE item)
        {
        }

        public void Validate(TE item)
        {
            validationIssues = new List<ValidationIssue>();
            
            Validating(item);

            if (validationIssues.Count > 0) throw new ValidationIssueException(validationIssues);
        }

        public void Validate(Action validations)
        {
            validationIssues = new List<ValidationIssue>();
            
            validations();

            if (validationIssues.Count > 0) throw new ValidationIssueException(validationIssues);
        }
    }
}
