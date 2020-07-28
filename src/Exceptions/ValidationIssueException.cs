using System;
using System.Collections.Generic;
using Acce.Domain;

namespace Acce.Exceptions
{
    public class ValidationIssueException : Exception
    {
        public IList<ValidationIssue> ValidationIssues { get; set; }

        public ValidationIssueException(ValidationIssue validationIssue) : base()
        {
            ValidationIssues = new List<ValidationIssue>();
            ValidationIssues.Add(validationIssue);
        }

        public ValidationIssueException(IList<ValidationIssue> validationIssues) : base()
        {
            ValidationIssues = validationIssues;
        }
    }
}