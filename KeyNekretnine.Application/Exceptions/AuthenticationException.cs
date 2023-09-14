namespace KeyNekretnine.Application.Exceptions;
public sealed class AuthenticationException : Exception
{
    public AuthenticationException(IEnumerable<AuthenticationError> errors)
    {
        Errors = errors;
    }

    public IEnumerable<AuthenticationError> Errors { get; }
}
