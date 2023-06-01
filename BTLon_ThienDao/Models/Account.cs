using Microsoft.AspNetCore.Mvc;
using System;   
using System.ComponentModel.DataAnnotations;    

namespace BTLon_ThienDao.Models
{
    public class Account
    {

        [Key]
        public int AccID { get; set; }
        [Required(ErrorMessage = "Ban phai nhap vao Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Ban phai nhap vao Password")]
        public string Password { get; set; }

    }
}
