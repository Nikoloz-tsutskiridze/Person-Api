using System.ComponentModel.DataAnnotations;

namespace Person.Core.Domains
{
    public class City
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
