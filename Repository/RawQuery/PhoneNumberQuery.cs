namespace Repository.RawQuery;
public class PhoneNumberQuery
{
    public const string GetCountryPhone = @"
        SELECT phone FROM phone_numbers WHERE id = @countryId";
}
