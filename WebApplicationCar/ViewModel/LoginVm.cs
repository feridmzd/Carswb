using System.ComponentModel.DataAnnotations;

namespace WebApplicationCar.ViewModel
{
    public class LoginVm
    {

        [Required]
        public string UserNameOrEmail { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
