﻿using System.ComponentModel.DataAnnotations;

namespace C64.FrontEnd.Models
{
    public class LoginInfo
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public bool Persistent { get; set; }
    }
}