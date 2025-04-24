using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Parcial3.Models;
using Parcial3.Services;
using Newtonsoft.Json.Linq; // Para crear objetos JSON
using System.Security.Claims;

namespace Parcial3.Controllers
{
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        private readonly AuthService authService;

        public AuthController()
        {
            this.authService = new AuthService();
        }

        [Route("login")]
        [HttpPost]
        public IHttpActionResult login(LoginModel logjn)
        {
            try
            {
                string token = authService.ValidarUsuario(logjn);
                if (token != null)
                {
                    // Obtener el idAdministradorITM para incluirlo en la respuesta
                    AdministradorITM user = authService.BuscarPorUsuario(logjn.Usuario);
                    if (user != null)
                    {
                        // Devolver el token y el idAdministradorITM en un objeto JSON
                        var jsonResponse = new JObject
                        {
                            { "token", token },
                            { "idAdministradorITM", user.idAministradorITM }
                        };
                        return Ok(jsonResponse); // Código 200 OK
                    }
                    else
                    {
                        return InternalServerError("Usuario no encontrado al generar la respuesta."); // Error interno
                    }
                }
                else
                {
                    return Unauthorized(); // Código 401 No Autorizado
                }
            }
            catch (Exception ex)
            {
                // Log the exception (important for debugging)
                Console.WriteLine($"Error in login: {ex.Message}");
                return InternalServerError(); // Código 500 Internal Server Error
            }
        }
    }
}