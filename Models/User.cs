using Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Models
{
    public class User
    {
        [Required(ErrorMessage = " * Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = " * Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = " * Address is required")]
        public string Address { get; set; }
        [Required(ErrorMessage = " * Phone is required")]
        public string Phone { get; set; }
        public string UserType { get; set; }
        [Required(ErrorMessage = " * Money is required")]
        public decimal Money { get; set; }
    }
}
