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
    }
}
