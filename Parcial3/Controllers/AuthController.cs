using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Parcial3.Models;
using Parcial3.Services;

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
        public string login(LoginModel logjn)
        {
            return authService.ValidarUsuario(logjn);
        }
    }
}
