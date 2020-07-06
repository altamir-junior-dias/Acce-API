using System.Collections.Generic;
using System.Linq;
using Acce.Domain;

namespace Acce.Controllers
{
    public class ResponseMessage
    {
        private string reference;
        private ResponseType responseType { get; }
        public string Reference
        {
            get
            {
                return responseType == ResponseType.DatabaseException ? "" : reference;
            }
        }
        public string Message
        {
            get
            {
                if (responseType == ResponseType.DatabaseException)
                {
                    return reference;
                }
                else
                {
                    if (responseType == ResponseType.DataNotFound) return string.Format("{0} not found.", reference);
                    if (responseType == ResponseType.DataNotProvided) return string.Format("The {0} data was not provided.", reference);
                    if (responseType == ResponseType.ValidationIssues) return string.Format("Some validation issues must be resolved: {0}", reference);

                    return "";
                }
            }
        }

        public ResponseMessage(ResponseType responseType, string reference)
        {
            this.responseType = responseType;
            this.reference = reference;
        }
        public ResponseMessage(ResponseType responseType, IList<ValidationIssue> validationIssues)
        {
            this.responseType = responseType;
            this.reference = string.Join(", ", validationIssues.Select(vi => vi.IssueType == IssueTypeEnum.PropertyNotInformed ? string.Format("{0} must be provided", vi.PropertyName) : vi.Message));
        }
    }
}
