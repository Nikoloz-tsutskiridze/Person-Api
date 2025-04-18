﻿using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using BasePerson.Core.Dtos;
using Person.Core.Domains;
using BasePerson.Core.Domains;

namespace Person.Core.Domains
{
    public enum Gender
    {
        Male,
        Female
    }

    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required, MinLength(2), MaxLength(50)]
        [RegularExpression(@"^[ა-ჰ]+$|^[A-Za-z]+$", ErrorMessage = "Name must contain only Georgian or Latin letters.")]
        public string Name { get; set; } = string.Empty;

        [Required, MinLength(2), MaxLength(50)]
        [RegularExpression(@"^[ა-ჰ]+$|^[A-Za-z]+$", ErrorMessage = "Last name must contain only Georgian or Latin letters.")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public Gender Gender { get; set; }

        [Required, StringLength(11)]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "Personal number must be exactly 11 digits.")]
        public string PersonalNumber { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [NotMapped]
        public bool IsAdult => DateTime.Now.Year - DateOfBirth.Year >= 18;

        public string? Img { get; set; }

        [Required]
        public int CityId { get; set; }
        public City? City { get; set; }

        public List<Phone> Phones { get; set; } = new List<Phone>();

        public ICollection<PhoneRelativePerson>? PhoneRelativePeople { get; set; }

        public ICollection<PeopleRelative>? FirstPersonRelatives { get; set; }
        public ICollection<PeopleRelative>? SecondPersonRelatives { get; set; }

        public ExistingCustomerDto ConvertToDto()
        {
            var dto = new ExistingCustomerDto()
            {
                Id = Id,
                Name = Name,
                LastName = LastName,
                Gender = (int)Gender,
                PersonalNumber = PersonalNumber,
                DateOfBirth = DateOfBirth,
                IsAdult = IsAdult,
                Img = Img
            };
            return dto;
        }
    }
}
