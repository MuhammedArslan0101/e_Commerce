using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace e_Commerce.Models
{
    public class ChangePasswordModel
    {
        [Required]
        [DisplayName("Old Password :")]
        public string OldPassword { get; set; }
        [Required]
        [StringLength(100 , MinimumLength =5 , ErrorMessage ="En Az 5 Charecter....")]
        [DisplayName("New Password :")]
        public string NewPassword { get; set; }
        [Required]
        [DisplayName("Conf Password :")]
        [Compare("NewPassword" ,ErrorMessage ="Not Match ......")]
        public string ConfNewPassword { get; set; }

    }
}