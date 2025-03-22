namespace BasePerson.Api.Dtos
{
    public class PhoneDto
    {
        public int Id { get; set; }
        public int Type { get; set; } 
        public string Number { get; set; }
        public int PersonId { get; set; }
    }
}
