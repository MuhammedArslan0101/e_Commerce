using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace e_Commerce.Models
{
    public class ShippingDetails
    {

        public string UserName { get; set; }
        [Required(ErrorMessage = "Plz add Address....")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Add City Name Plz..")]
        public string City { get; set; }
        [Required(ErrorMessage = "Add District Plz ..")]
        public string District { get; set; }
        [Required(ErrorMessage = "Add Neghborhood plz ..")]
        public string Neghborhood { get; set; }
        [Required(ErrorMessage = "Add PostCode Plz..")]
        public string PostaCode { get; set; }
    }
}