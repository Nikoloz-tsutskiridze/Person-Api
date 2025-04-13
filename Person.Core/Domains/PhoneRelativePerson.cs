using BasePerson.Core.Dtos;
using Person.Core.Domains;

namespace BasePerson.Core.Domains
{
    public class PhoneRelativePerson
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public Customer? Person { get; set; }
        public int PhoneId { get; set; }
        public Phone? Phone { get; set; }
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
