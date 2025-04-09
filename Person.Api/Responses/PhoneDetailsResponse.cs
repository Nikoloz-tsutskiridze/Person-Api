﻿using BasePerson.Api.Domains;
using Person.Api.Domains;

namespace BasePerson.Api.Responses
{
    public class PhoneDetailsResponse
    {
        public string Number { get; set; }
        public PhoneType Type { get; set; }
        public int PhoneId { get; set; }
        public int ConnectionId { get; set; }
    }

    public class PersonDetailsResponse
    {
        public PersonDetailsResponse(PersonType personType, int connectedWith, int connnectionId)
        {
            Type = personType;
            ConnectedWith = connectedWith;
            ConnectionId = connnectionId;   
        }
        public PersonType Type { get; set; }
        public int ConnectedWith { get; set; }
        public int ConnectionId { get; set; }
    }
}
