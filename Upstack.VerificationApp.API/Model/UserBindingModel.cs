using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Upstack.VerificationApp.API.Model
{
    public class UserBindingModel
    {
        public Int64 Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public bool IsRegistered { get; set; }
        public DateTime DateRegistered { get; set; }
        public DateTime DateConfirmed { get; set; }
    }
}
