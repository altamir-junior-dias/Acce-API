namespace Bootstrapping.Controllers
{
    public enum ResponseType
    {
        Exception = 0,
        DatabaseException = 1,
        ContentBodyNotProvided = 2,
        ParameterNotProvided = 3,
        PropertyNotProvided = 4,
        DataNotFound = 5,
        DataNotProvided = 6,
        RoleFail = 7
    }
}