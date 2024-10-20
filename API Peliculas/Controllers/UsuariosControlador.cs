using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API_Peliculas.Models;
using API_Peliculas.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace API_Peliculas.Controllers
{
    [Route("api/[controller]/[action]")]
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


        [HttpGet]
        public IActionResult ConsultarUsers()
        {
            var resultado = _context.pe_users.ToList();

            return new JsonResult(Ok(resultado));
        }

        [HttpPost]
        public IActionResult Login([FromBody] MLogin loginData)
        {
            var user = _context.pe_users.SingleOrDefault(u => u.us_usuario == loginData.Usuario && u.us_pass == loginData.Pass);

            if(user != null)
            {
                //Crea la informacion del usuario
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.us_usuario),
                    new Claim(ClaimTypes.NameIdentifier, user.us_id.ToString())
                };

                // Crear las caracteristicas principales de autenticación
                var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true, // La sesión será persistente
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(30) // Tiempo de expiración(esta en utc porque no queria buscar como poner otra)
                };

                // Autenticar al usuario
                HttpContext.SignInAsync("CookieAuth", new ClaimsPrincipal(claimsIdentity), authProperties);

                return Ok("Login exitoso");
            }
            else
            {
                return Unauthorized("Credenciales incorrectas");
            }
        }
        //Pense que seria mas dificil este
        //POS:Ver si deja rastro en la cookie
        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync("CookieAuth");
            return Ok("Sesión cerrada correctamente");
        }

    }

}
