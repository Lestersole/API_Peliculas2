using System.ComponentModel.DataAnnotations;

namespace API_Peliculas.Models
{
    public class MPeliculas
    {
        [Key]
        public int pe_id {  get; set; }
        public string pe_nombre { get; set; }
        public string pe_descripcion { get; set; }
        public string pe_categoria1 { get; set; }
        public TimeOnly pe_duracion { get; set; }


    }
}
