namespace Entities.Exceptions;
public sealed class AuthenticationException : Exception
{
    public IReadOnlyDictionary<string, string> Errors { get; }
    public AuthenticationException(IReadOnlyDictionary<string, string> errors)
    : base("One or more validation errors occurred")
    => Errors = errors;
}