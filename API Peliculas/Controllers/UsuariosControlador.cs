using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API_Peliculas.Models;
using API_Peliculas.Data;

namespace API_Peliculas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosControlador : ControllerBase
    {
        private readonly APIContext _context;

        public UsuariosControlador(APIContext context)
        {
            _context = context;
        }

        [HttpPost]
        public JsonResult CrearUser(MUsuarios usuarios)
        {
            if (usuarios.us_id >= 0)
            {
                _context.pe_users.Add(usuarios);
            }
            else
            { 
                var userInDb = _context.pe_users.Find(usuarios.us_id);

                if (userInDb == null)
                    return new JsonResult(NotFound());

                userInDb = usuarios;

            }

            _context.SaveChanges();

            return new JsonResult(Ok(usuarios));
        }
    }
}
