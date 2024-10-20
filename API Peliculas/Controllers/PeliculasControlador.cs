using API_Peliculas.Data;
using Microsoft.AspNetCore.Mvc;

namespace API_Peliculas.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PeliculasControlador : ControllerBase
    {
        private readonly APIContext _context;

        public PeliculasControlador(APIContext context)
        {
            _context = context;
        }

        //Get
        [HttpGet]

        public JsonResult Get(int id)
        {
            var resultado = _context.pe_peliculas.Find(id);

            if (resultado == null)
                return new JsonResult(NotFound());


            return new JsonResult(Ok(resultado));
        }

        [HttpGet]
        public IActionResult GetPeliculas()
        {
            var resultado = _context.pe_peliculas.ToList();

            return new JsonResult(Ok(resultado));

        }

        [HttpGet]
        public IActionResult GetCategoria(string categoria)
        {
            var resultado = _context.pe_peliculas.Where
                    (p => p.pe_categoria1 == categoria ||
                    p.pe_categoria2 == categoria ||
                    p.pe_categoria3 == categoria).ToList();

            if (resultado == null || !resultado.Any())
                return new JsonResult(NotFound());

            return new JsonResult(Ok(resultado));
        }
        [HttpGet]
        public IActionResult GetPreferenciasUsuario( string usuario)
        {
            var resultado = _context.pe_users
                                .Where(p => p.us_usuario == usuario)
                                .Select(p => p.us_preferencia)
                                .ToList();

            if (resultado == null || !resultado.Any())
                return new JsonResult(NotFound());

            var peliculas = _context.pe_peliculas
                .Where(p => resultado.Contains(p.pe_categoria1) ||
                            resultado.Contains(p.pe_categoria2) ||
                            resultado.Contains(p.pe_categoria3))
                .ToList();

            if (peliculas == null || !peliculas.Any())
                return new JsonResult(NotFound());

            return new JsonResult(Ok(peliculas));
        }
    }
}
