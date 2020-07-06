namespace Acce.Controllers
{
    public enum ResponseType
    {
        Exception = 0,
        DatabaseException = 1,
        ValidationIssues = 2,
        DataNotFound = 3,
        DataNotProvided = 4,
        RoleFail = 5
    }
}