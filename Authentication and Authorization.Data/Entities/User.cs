﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace Authentication_and_Authorization.Data.Entities
{
    [Index(nameof(Email), IsUnique = true)]
    public class User : EntityBase
    {
        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }

        public UserType UserType { get; set; }
    }

    public enum UserType
    {
        Admin = 1, Customer 
    }
}
