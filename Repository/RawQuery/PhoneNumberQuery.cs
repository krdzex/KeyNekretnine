namespace Repository.RawQuery;
public class PhoneNumberQuery
{
    public const string AllPhoneNumbersQuery = @"
        SELECT id,code,label,phone FROM phone_numbers";
}