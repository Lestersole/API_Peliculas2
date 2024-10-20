using API_Peliculas.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Peliculas.Data
{
    public class APIContext : DbContext
    {
        public APIContext(DbContextOptions<APIContext> options) : base(options) { 
        
        }
        //Donde van todos los modelos
        public DbSet<MPeliculas> pe_peliculas { get; set; }

        public DbSet<MUsuarios> pe_users { get; set; }
    }
}
