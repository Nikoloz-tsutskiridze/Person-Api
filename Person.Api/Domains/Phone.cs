﻿namespace Person.Api.Domains
{
    public enum PhoneType
    {
        Mobile,
        Office,
        Home
    }

    public class Phone
    {
        public int Id { get; set; }
        public PhoneType Type { get; set; }
        public string Number { get; set; }
    }
}
