using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace e_Commerce.Models
{
    public class Register
    {
        [Required]
        [DisplayName("Name :")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Surname :")]
        public string Surname { get; set; }
        [Required]
        [DisplayName("UserName :")]
        public string Username { get; set; }
        [Required]
        [DisplayName("Email :")]
        [EmailAddress(ErrorMessage ="Error email.....")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Password :")]
        //[MembershipPassword(
        //      MinRequiredNonAlphanumericCharacters = 1,
        //      MinNonAlphanumericCharactersError = "Your password needs to contain at least one symbol (!, @, #, etc).",
        //      ErrorMessage = "Your password must be 6 characters long and contain at least one symbol (!, @, #, etc).",
        //      MinRequiredPasswordLength = 6)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("ConfPassword :")]
        [Compare("Password" , ErrorMessage ="Password not match ...")]
        public string RePassword { get; set; }

    }
}