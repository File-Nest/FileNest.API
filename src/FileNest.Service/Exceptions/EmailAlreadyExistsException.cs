namespace FileNest.Service.Exceptions
{
    public class EmailAlreadyExistsException : AppException
    {
        public EmailAlreadyExistsException(string email)
            : base($"A user with the email '{email}' already exists.",409)
        {
        }
    }
}