namespace Bootstrapping.Controllers
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
                    if (responseType == ResponseType.ContentBodyNotProvided) return string.Format("{0} must be provided on body.", reference);
                    if (responseType == ResponseType.ParameterNotProvided) return string.Format("{0} must be provided on parameters.", reference);
                    if (responseType == ResponseType.PropertyNotProvided) return string.Format("The {0} property was not provided.", reference);

                    return "";
                }
            }
        }

        public ResponseMessage(ResponseType responseType, string reference)
        {
            this.responseType = responseType;
            this.reference = reference;
        }
    }
}
