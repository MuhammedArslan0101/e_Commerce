using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace e_Commerce.Models
{
    public class UserCard
    {
        [Required]
        public string CardName { get; set; }
        [Required]
        public string CardNum { get; set; }
        [Required]
        public int Cvc { get; set; }
        [Required]
        public int ExpMonth { get; set; }
        [Required]
        public int Expyear { get; set; }
    }
}