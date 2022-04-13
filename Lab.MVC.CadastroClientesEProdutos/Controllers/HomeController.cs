using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab.MVC.CadastroClientesEProdutos.Controllers
{
    public class HomeController : Controller
    {
        // Criando Método de Erro.cshtml
        public ActionResult MostrarErro()
        {
            ViewBag.MensagemErro = "Erro interno no servidor.";
            return View("Erro");
        }

        // Método que aparecerá na view correspondente.
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Sobre()
        {
            ViewBag.Message = "Um pouco de nossa História...";

            return View();
        }

        public ActionResult Contato()
        {
            ViewBag.Message = "Nossa página de contato.";

            return View();
        }
    }
}