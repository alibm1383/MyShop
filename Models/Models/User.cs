using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Models.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(200)]
        public required string Email { get; set; }
        [MaxLength(50)]
        public required string Password { get; set; }   
        public DateTime RegisteredDate { get; set; }
        public bool IsAdmin { get; set; }


        public List<Order>? Orders { get; set; }
    }
}
