﻿namespace Zadatak1.ViewModels
{
    public class UserEditViewModel
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public bool Role { get; set; }
        public bool LoginPermission { get; set; }
    }
}
