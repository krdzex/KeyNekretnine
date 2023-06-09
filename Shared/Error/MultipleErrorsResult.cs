namespace Shared.Error
{
    public sealed class MultipleErrorsResult : Result, IMultipleErrorsResult
    {
        private MultipleErrorsResult(Error[] errors)
            : base(false, IMultipleErrorsResult.ValidationError) =>
            Errors = errors;

        public Error[] Errors { get; }
        public static MultipleErrorsResult WithErrors(Error[] errors) => new(errors);
    }

}
