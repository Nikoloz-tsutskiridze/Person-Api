using Person.Api.Domains;

namespace BasePerson.Api.Dtos
{
    public class PhoneDto : PhoneContentDto
    {
        public int Id { get; set; }
    }

    public class PhoneContentDto
    {
        public PhoneType Type { get; set; }
        public string? Number { get; set; }
    }
}
