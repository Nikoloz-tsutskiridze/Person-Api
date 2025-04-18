﻿using BasePerson.Core.Domains;

namespace BasePerson.Core.Dtos
{
    public class PeopleRelativeDto
    {
        public int FirstPersonId { get; set; }
        public int SecondPersonId { get; set; }
        public PersonType ConnectionType { get; set; }
    }

    public class ExsitingPeopleRelativeDto : PeopleRelativeDto
    {
        public int Id { get; set; }
    }
}
