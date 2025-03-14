using System.ComponentModel.DataAnnotations;

namespace Person.Api.Domains
{
    public class City
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
