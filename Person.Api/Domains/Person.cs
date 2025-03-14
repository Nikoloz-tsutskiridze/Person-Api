using System.ComponentModel.DataAnnotations;

namespace Person.Api.Domains
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string PersonalNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Img { get; set; }
        public List<Phone> Phone { get; set; }
        public City City { get; set; }
    }
}
