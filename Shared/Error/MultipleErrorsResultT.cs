namespace Shared.Error
{
    public sealed class MultipleErrorsResult<TValue> : Result<TValue>, IMultipleErrorsResult
    {
        private MultipleErrorsResult(Error[] errors)
            : base(default, false, IMultipleErrorsResult.ValidationError) =>
            Errors = errors;

        public Error[] Errors { get; }
        public static MultipleErrorsResult<TValue> WithErrors(Error[] errors) => new(errors);
    }
}
