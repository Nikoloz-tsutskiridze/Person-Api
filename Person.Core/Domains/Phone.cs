using BasePerson.Core.Domains;
using BasePerson.Core.Dtos;
using System.Numerics;

namespace Person.Core.Domains
{
    public enum PhoneType
    {
        Mobile,
        Office,
        Home
    }

    public class Phone
    {
        public int Id { get; set; }
        public PhoneType Type { get; set; }
        public string Number { get; set; } = null!;
        public ICollection<PhoneRelativePerson>? PhoneRelativePeople { get; set; }
        public PhoneDto ConvertToDto()
        {
            var phoneDto = new PhoneDto
            {
                Id = Id,
                Type = Type,
                Number = Number
            };

            return phoneDto;
        }
    }
}
