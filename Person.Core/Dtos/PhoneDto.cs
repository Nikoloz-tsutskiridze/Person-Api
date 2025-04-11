using Person.Core.Domains;

namespace BasePerson.Core.Dtos
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
