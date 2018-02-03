using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.DataAnnotations;


namespace Opera.Models
{
    public class ProfileUpdate
    {
        [DataType(DataType.Text)]
        public string FullName { get; set; }   

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile Image { get; set; }
    }
}