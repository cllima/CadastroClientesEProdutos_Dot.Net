using Lab.MVC.CadastroClientesEProdutos.Data;
using Lab.MVC.CadastroClientesEProdutos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab.MVC.CadastroClientesEProdutos.Controllers
{
    public class ItensController : Controller
    {
        // GET Itens
        public ActionResult Index()
        {
            return View();
        }

        // Incluir GET
        [HttpGet]
        public ActionResult Incluir(int? idPedido)
        {
            try
            {
                ViewBag.ListaDeProdutos = new SelectList(
                ProdutosDao.ListaProduto(), "Id", "Descricao");
                ViewBag.ListaDePedidos = new SelectList(
                ItensDao.ListarPedidos(), "IdPedido", "NomeCliente");
                ViewBag.ListaDeItens = ItensDao.ListarItensPorPedido(idPedido);
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.MensagemErro = ex.Message;
                return View("_Erro");
            }
        }

        // Incluir Post
        [HttpPost]
        public ActionResult Incluir(Item item, int? idPedido)
        {
            if (!ModelState.IsValid)
            {
                return Incluir(idPedido);
            }

            try
            {
                item.IdPedido = (int)idPedido;
                ItensDao.IncluirItem(item);
                return RedirectToAction("Incluir", new
                {
                    idPedido = (int)idPedido
                });
            }
            catch (Exception ex)
            {
                ViewBag.MensagemErro = ex.Message;
                return View("_Erro");
            }
        }

        // Criar a Lista de Itens
        public ActionResult Listar(string id)
        {
           //return View(ItensDao.ListarPedidos());

            try
            {
                ViewBag.ListaDeItens = new SelectList(ItensDao.ListarItensPorPedido());
                return View(ItensDao.ListarItensPorPedido());
            }
            catch (Exception ex)
            {
                ViewBag.MensagemErro = ex.Message;
                return View("Erro");
            }

        }


        // Remover Item.
        public ActionResult Remover(int? id)
        {
            try
            {
                if (id == null)
                {
                    throw new Exception("Código não informado");
                }
                var item = ItensDao.BuscarItem((int)id);
                if (item == null)
                {
                    throw new Exception("Item não encontrado");
                }
                int idPedido = item.IdPedido;
                ItensDao.RemoverItem(item);
                return RedirectToAction("Incluir", new
                {
                    idPedido
                });
            }
            catch (Exception ex)
            {
                ViewBag.MensagemErro = ex.Message;
                return View("_Erro");
            }
        }

    }
}