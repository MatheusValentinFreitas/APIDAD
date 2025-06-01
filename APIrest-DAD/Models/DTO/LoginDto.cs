using System.ComponentModel.DataAnnotations;

namespace APIrest_DAD.Models.DTO
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Campo cliente obrigatório")]
        public string client { get; set; }

        [Required(ErrorMessage = "Campo token obrigatório")]
        public string token { get; set; }
    }
}
