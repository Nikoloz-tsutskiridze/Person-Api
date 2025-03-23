using BasePerson.Api.Dtos;
using Person.Api.Domains;

namespace BasePerson.Api.Domains
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
