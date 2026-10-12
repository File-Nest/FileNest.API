namespace FileNest.Service.Exceptions
{
    public class UserExceptions : Exception
    {
        public int StatusCode { get; }
        public UserExceptions(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
        public static UserExceptions EmailAlreadyExists(string email)
        {
            return new UserExceptions(
                $"A user with the email '{email}' already exists.",409);
        }
    }
}