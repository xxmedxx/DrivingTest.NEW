﻿
using System;
using System.Collections.Generic;

namespace App.Models
{
    public class AppUserModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
