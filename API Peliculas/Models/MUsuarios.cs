using System.ComponentModel.DataAnnotations;

namespace API_Peliculas.Models
{
    public class MUsuarios
    {
        [Key]
        public int us_id { get; set; }
        public string us_usuario { get; set; }
        public string us_pass { get; set; }
        public string us_mail { get; set; }
        public string us_preferencia { get; set; }
    }
}
