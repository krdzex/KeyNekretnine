namespace Entities.Exceptions;
public sealed class UserNotFoundException : NotFoundException
{
    public UserNotFoundException()
        : base($"User doesn't exist in the database.")
    {
    }
}