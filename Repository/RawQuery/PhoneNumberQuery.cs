namespace Repository.RawQuery;
public class PhoneNumberQuery
{
    public const string AllPhoneNumbersQuery = @"
        SELECT id,code,label,phone FROM phone_numbers";

    public const string GetCountryPhone = @"
        SELECT phone FROM phone_numbers WHERE id = @countryId";
}
