using BasePerson.Api.Responses;

namespace BasePerson.Api.Dtos
{
    public class CustomerDto 
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Gender { get; set; }  
        public string PersonalNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsAdult { get; set; }
        public int CityId { get; set; }
        public string? Img { get; set; }
        
    }

    public class ExistingCustomerDto : CustomerDto
    {
        public int Id { get; set; }
        public List<PhoneDetailsResponse>? Phones { get; set; }
        public List<PersonDetailsResponse> ConnectedPeople { get; set; }
    }
}
