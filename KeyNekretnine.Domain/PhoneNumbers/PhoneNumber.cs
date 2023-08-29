using KeyNekretnine.Domain.Models;

namespace KeyNekretnine.Domain.PhoneNumbers;
public class PhoneNumber : EntityBase
{
    public string Code { get; set; }
    public string Label { get; set; }
    public string Phone { get; set; }
}