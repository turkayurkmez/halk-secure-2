using System.ComponentModel.DataAnnotations;

namespace JWTWithRest
{
    public class UserLogin
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }


    }
}
