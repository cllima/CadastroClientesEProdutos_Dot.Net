using Microsoft.Owin;
using Owin;
using System;
using System.Threading.Tasks;

// Adcionar as Classes abaixo para que seja possível configurar o 
// Identity e OWIN
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Cookies;

/*
 * Instalar a cada projeto  =>_UsuarioPV
    asp.net identity entity
    asp.net identity  owin
    owin host
*/

[assembly: OwinStartup(typeof(Lab.MVC.CadastroClientesEProdutos.Startup))]

namespace Lab.MVC.CadastroClientesEProdutos
{
    // Escreve o método abaixo.
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Configurar o tipo de autenticação.
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Usuarios/Login"),
                LogoutPath = new PathString("/Usuarios/Logout")

            });
        }
    }
}
