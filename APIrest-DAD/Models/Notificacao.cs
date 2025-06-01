using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIrest_DAD.Models
{
    [Table("SERV_NOTIFICACAO")]
    public class Notificacao
    {
        [Key]
        public int codigoNotificacao { get; set; }

        [Required(ErrorMessage = "Codigo usuario obrigatório")]
        public int codigoUsuario { get; set; }

        [Required(ErrorMessage = "Campo nome obrigatório")]
        public string nome { get; set; }

        [Required(ErrorMessage = "Campo e-mail obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        public string email { get; set; }

        [Required(ErrorMessage = "Data de notificação obrigatório")]
        public DateTime dataNotificacao { get; set; }

        public int status {  get; set; }

        public string? dataPreNotificacao { get; set; }
    }
}
