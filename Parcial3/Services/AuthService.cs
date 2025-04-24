using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Parcial3.Models;
using Servicios_Jue.Service;

namespace Parcial3.Services
{
    public class AuthService
    {

        private readonly DBExamenEntities _context;
        public AuthService()
        {
            _context = new DBExamenEntities();
        }

        public static string GenerarHash(string textoPlano)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(textoPlano);
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        public static bool ValidarHash(string textoPlano, string hashAlmacenado)
        {
            string hashNuevo = GenerarHash(textoPlano);
            return hashNuevo == hashAlmacenado;
        }

        public string ValidarUsuario(LoginModel login)
        {
            AdministradorITM user = _context.AdministradorITM.FirstOrDefault(a => a.Usuario == login.Usuario);
            if (user != null)
            {
                if (ValidarHash(login.Clave, user.Clave))
                {
                    return TokenGenerator.GenerateTokenJwt(user.Documento);
                }
            }
            else
            {
            }
            return "Credenciales incorrectas";
        }

        public AdministradorITM BuscarPorCedula(string cedula)
        {
            return _context.AdministradorITM.FirstOrDefault(a => a.Documento == cedula);
        }
    }
}