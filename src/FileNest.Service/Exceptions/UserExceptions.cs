namespace FileNest.Service.Exceptions
{
    public class EmailAlreadyExistException : AppException
    {
        public EmailAlreadyExistException(string email)
            : base($"A user with the email '{email}' already exists.",409)
        {
        }
    }
}