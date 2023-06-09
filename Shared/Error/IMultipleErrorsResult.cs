namespace Shared.Error
{
    public interface IMultipleErrorsResult
    {
        public static readonly Error ValidationError = new(
            "MultipleErrors",
            "Multiple errors occurred.");

        Error[] Errors { get; }
    }
}
