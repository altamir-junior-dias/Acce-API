namespace Acce.Domain
{
    public class ValidationIssue
    {
        public string PropertyName { get; set; }
        public IssueTypeEnum IssueType { get; set; }
        public string Message { get; set; }
    }
}