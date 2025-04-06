using BasePerson.Api.Dtos;

namespace BasePerson.Api.Domains
{
    public enum PersonType
    {
        Colleague,
        Friend,
        Other
    }
    public class PeopleRelative
    {
        public int Id { get; set; }
        public int FirstPersonId { get; set; }
        public int SecondPersonId { get; set; }
        public PersonType ConnectionType { get; set; }

        public ExsitingPeopleRelativeDto ConvertToDto()
        {
            var Dto = new ExsitingPeopleRelativeDto
            {
                Id = Id,
                FirstPersonId = FirstPersonId,
                SecondPersonId = SecondPersonId,
                ConnectionType = ConnectionType,
            };

            return Dto;
        }
    }
}
