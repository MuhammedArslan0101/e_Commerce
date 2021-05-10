using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace e_Commerce.Models
{
    public class UserProfile
    {
        public string id { get; set; }
        [Required]
        [DisplayName("Name :")]
        public string Name { get; set; }
        [Required]
        [DisplayName("SurName :")]
        public string Surname { get; set; }
        [Required]
        [DisplayName("User Name")]
        public string Username { get; set; }
        [Required]
        [DisplayName("Email :")]
        [EmailAddress(ErrorMessage = "Faild Email..")]
        public string Email { get; set; }
    }
}