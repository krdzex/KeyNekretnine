using Shared.Error;

namespace Entities.DomainErrors
{
    public static partial class DomainErrors
    {
        public static class EmailService

        {
            public static Error EmailSendFailed => new Error(
                   "EmailService.Failed",
                   "There was error during email send");
        }
    }
}
