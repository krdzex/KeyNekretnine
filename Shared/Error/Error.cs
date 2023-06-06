namespace Shared.Error;
public sealed class Error
{
    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }

    public string Code { get; }

    public string Message { get; }

    internal static Error None => new Error(string.Empty, string.Empty);

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        Error other = (Error)obj;
        return Code == other.Code && Message == other.Message;
    }

    public override int GetHashCode()
    {
        return Code.GetHashCode() ^ Message.GetHashCode();
    }
}