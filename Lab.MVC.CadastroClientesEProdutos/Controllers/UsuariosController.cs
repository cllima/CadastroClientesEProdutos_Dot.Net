using Lab.MVC.CadastroClientesEProdutos.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab.MVC.CadastroClientesEProdutos.Controllers
{
    public class UsuariosController : Controller
    {
        // GET: Usuarios
        public ActionResult CriarUsuario()
        {
            return View();
        }

        // Http Post.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CriarUsuario(UsuarioViewModel usuario)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }


            try
            {
                // Vamos usar o Identity para criar o Usuário.
                var usuarioStore = new UserStore<IdentityUser>();

                // Objeto para Gerenciar os usuarios.
                var usuarioManager = new UserManager<IdentityUser>(usuarioStore);

                // Criamos uma Identidade do usuario = usuario  Info.
                var usuarioInfo = new IdentityUser()
                {
                    UserName = usuario.Nome
                   // Email = usuario.Email
                };

                IdentityResult resultado = usuarioManager.Create(usuarioInfo, usuario.Senha);

                if (resultado.Succeeded)
                {
                    //Autentica e volta para a página inicial já conectado.
                    var autManager = System.Web.HttpContext.Current.GetOwinContext().Authentication;
                    var identidadeUsuario = usuarioManager.CreateIdentity(usuarioInfo,
                    DefaultAuthenticationTypes.ApplicationCookie);
                    autManager.SignIn(new AuthenticationProperties() { },
                    identidadeUsuario);

                    // Caso queira que o usuário se conecte com a senha, tira a parte de cima.
                    return RedirectToAction("Index", "Home"); // Home - > Colocar Login vai para Pag. de Login
                }
                else
                {
                    throw new Exception(resultado
                    .Errors.FirstOrDefault());
                }
            }
            catch (Exception ex)
            {

                ViewBag.MensagemErro = ex.Message;
                return View("Erro");
            }
        }


        //Método Logout.
        [HttpGet]
        public ActionResult Logout()
        {
            // Obtém o cookie do usuário autenticado no momento.
            var autenticacao = System.Web.HttpContext.Current.GetOwinContext().Authentication;

            // Vamos fazer o Logout do usuário Atual.
            autenticacao.SignOut();

            // Retorna o usuário para a View principal  dentro da Controller HOME.
            return RedirectToAction("Index", "Home");

        }

        // Método Login.
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel usuario, string url)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                var usuarioStore = new UserStore<IdentityUser>();
                var usuarioManager = new UserManager<IdentityUser>(usuarioStore);
                var usuarioInfo = usuarioManager.Find(usuario.Nome, usuario.Senha);

                // Sempre valida usuarioInfo.
                if (usuarioInfo != null)
                {
                    var gerenciarAutenticacao = System.Web.HttpContext.Current.GetOwinContext().Authentication;

                    // Vamos criar uma estrutura de identiifcação do usuário.
                    var identidadeUsuario = usuarioManager.CreateIdentity(usuarioInfo, DefaultAuthenticationTypes.ApplicationCookie);

                    // Faremos a autenticação do usuário no Sistema.
                    gerenciarAutenticacao.SignIn(new AuthenticationProperties()
                    {
                        IsPersistent = false
                    }, identidadeUsuario
                    );

                    return url == null ? Redirect("/Home/Index") : Redirect(url);
                }
                else
                {
                    // Aqui aparece sem a mensagem na Tela.
                    //throw new Exception("Usuário ou Senha inválidos");

                    // Aqui vai aparecer as mensagem na Tela e é necessário colocar na Pag. Login => o Método: if (ViewBag.MensagemErro != null)...
                    ViewBag.MensagemErro = "Usuário ou Senha inválidos";
                    ModelState.AddModelError("Nome", "Confira seu usuario e senha");
                    ModelState.AddModelError("Senha", "Confira seu usuario e senha");

                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.MensagemErro = ex.Message;
                return View("Erro");
            }
        }
    } 
}