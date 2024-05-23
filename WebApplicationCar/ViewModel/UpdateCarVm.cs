using System.ComponentModel.DataAnnotations;

namespace WebApplicationCar.ViewModel
{
    public class UpdateCarVm
    {


        public int  Id { get; set; }

        [Required]
        [MaxLength(100)]
        [MinLength(5)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        [MinLength(5)]
        public string Description { get; set; }

        public string? ImgUrl { get; set; }
        public IFormFile ImgFile { get; set; }


    }
}
