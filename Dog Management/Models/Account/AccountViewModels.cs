using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dog_Management.Models
{
    public class LoginModel
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }
    public class RegisterModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Name { get; set; }

    }
    public class NewUserInfoModel
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public string Region { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public string Telephone { get; set; }
    }
}