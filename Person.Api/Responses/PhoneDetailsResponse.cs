using Person.Api.Domains;

namespace BasePerson.Api.Responses
{
    public class PhoneDetailsResponse
    {
        public string Number { get; set; }
        public PhoneType Type { get; set; }
        public int PhoneId { get; set; }
        public int ConnectionId { get; set; }
    }
}
