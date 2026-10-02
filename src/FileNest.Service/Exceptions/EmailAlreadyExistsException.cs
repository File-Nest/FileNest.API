using Microsoft.AspNetCore.Http;

namespace FileNest.Service.Exceptions
{
    public class EmailAlreadyExistsException : AppException
    {
        public EmailAlreadyExistsException(string email)
            : base(
                $"A user with the email '{email}' already exists.",
                StatusCodes.Status409Conflict)
        {
        }
    }
}