using Lab.MVC.CadastroClientesEProdutos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lab.MVC.Data;

namespace Lab.MVC.CadastroClientesEProdutos.Controllers
{
    public class ClientesController : Controller
    {
        // GET: Clientes
        [HttpGet]
        public ActionResult Index() // Criar uma controller - click na index .
        {
            return View();
        }

        [HttpGet]
        [Authorize] // Somente permite acesso se estiver logado.
        public ActionResult Incluir()
        {
            // Retorna o HTmL do formulário para incluir o cliente.
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Validar o request do formulário.
        public ActionResult Incluir(Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                // Se for invalido retorna para a view de origem.
                ModelState.AddModelError(string.Empty, "Por favor verifique seus dados.");
                return View();

            }

            try
            {
                // Incluimos o cliente na Base.
                // Para isso precisamos fazer os import da pasta DATA.
                ClientesDao.IncluirCliente(cliente);
                return RedirectToAction("Listar");
            }

            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    ViewBag.MensagemErro = "Erro a verificar: " + ex.InnerException.Message;
                }

                ViewBag.MensagemErro = ViewBag.MensagemErro + "\n " + ex.Message;
                return View("Erro");
            }
        }

        [HttpGet]
        public ActionResult Listar()
        {
            // Fazendo o Select no BD e obtendo a lista de clientes.
            var resultado = ClientesDao.ListarClientes();

            // Retorna o objeto de lista já preenchido para a View.
            return View(resultado);

            // Forma simplificada.
           // return View(ClientesDao.ListarClientes());
        }


        // Função:
        //Criando Método Buscar, Detalhes e Excluir.
        private ActionResult Buscar(string doc, string nomeDaView)
        {
            try
            {
                if (doc == null)
                {
                    // Lançar um exception (ERRO)
                    throw new Exception("O documento não é válido.");
                }

                // Se o documento é diferente de nulo, buscamos o cliente na Base,
                var cliente = ClientesDao.BuscarCliente(doc);

                if (cliente == null)
                {
                    throw new Exception("O cliente não foi localizado.");
                }

                return View(nomeDaView, cliente);

            }
            catch (Exception ex)
            {
                ViewBag.MensagemErro = ex.Message;
                return View("Erro");
            }
        }


        [Authorize] // Somente permite acesso se estiver logado.
        public ActionResult Editar(string id)
        {
            return Buscar(id, "Editar");
        }

        // Criando função para Editar..
        [HttpPost]
        public ActionResult Editar(Cliente cliente)
        {
            try
            {
                ClientesDao.AlterarCliente(cliente);

                return RedirectToAction("Listar");
            }
            catch (Exception ex)
            {
                ViewBag.MensagemErro = ex.Message;
                return View("_Erro");
            }
        }


        public ActionResult Detalhes(string id)
        {
            return Buscar(id, "Detalhes");
        }

        
        public ActionResult Excluir(string id)
        {
            return Buscar(id, "Excluir");
        }

        // Criando função para Excluir..
        [HttpPost]
        [Authorize] // Somente permite acesso se estiver logado.
        public ActionResult Excluir(Cliente cliente)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(cliente.Documento))
                {
                    ModelState.AddModelError(string.Empty, "Não foi possível excluir o registro");
                    return View();
                }

                cliente = ClientesDao.BuscarCliente(cliente.Documento);

                if (cliente == null)
                {
                    ModelState.AddModelError(string.Empty, "Não foi possível excluir o registro");
                    return View();
                }

                // remove o cliente
                ClientesDao.RemoverCliente(cliente);

                return RedirectToAction("Listar");
            }
            catch (Exception ex)
            {
                ViewBag.MensagemErro = ex.Message;
                return View("Erro");
            }
        }

    }
}