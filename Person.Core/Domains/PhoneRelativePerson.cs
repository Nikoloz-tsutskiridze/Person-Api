using BasePerson.Core.Dtos;

namespace BasePerson.Core.Domains
{
    public class PhoneRelativePerson
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int PhoneId { get; set; }
        public PhoneRelativePersonDto ConvertToDto()
        {
            var dto = new PhoneRelativePersonDto()
            {
               PersonId = PersonId,
               PhoneId = PhoneId,
            };
            return dto;
        }
    }
}
