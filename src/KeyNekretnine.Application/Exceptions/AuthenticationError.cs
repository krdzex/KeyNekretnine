namespace KeyNekretnine.Application.Exceptions;
public sealed record AuthenticationError(string PropertyName, string ErrorMessage);