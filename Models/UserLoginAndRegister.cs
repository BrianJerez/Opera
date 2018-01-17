using System.ComponentModel.DataAnnotations;

namespace Opera.Models
{
    public class UserLoginAndRegister
    {
        [DataType(DataType.Text)]
        public string UserName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }   
    }
}