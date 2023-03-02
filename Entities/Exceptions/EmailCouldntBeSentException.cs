namespace Entities.Exceptions;
public sealed class EmailCouldntBeSentException : BadRequestException
{
    public EmailCouldntBeSentException()
        : base("Email couldnt be send right now")
    {
    }
}