using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIrest_DAD.Models
{
    [Table("OAUTH_TOKEN")]
    public class OauthToken
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "Campo cliente obrigatório")]
        public string client {  get; set; }

        [Required(ErrorMessage = "Campo token obrigatório")]
        public string token { get; set; }

        public DateTime updated_at { get; set; } = DateTime.Now;

        public DateTime expires_at { get; set; } = DateTime.Now.AddYears(1);

        public DateTime created_at { get; set; } = DateTime.Now;
    }
}
