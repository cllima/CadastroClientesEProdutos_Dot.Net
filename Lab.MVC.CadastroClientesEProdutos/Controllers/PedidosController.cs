using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Lab.MVC.CadastroClientesEProdutos.Data;
using Lab.MVC.CadastroClientesEProdutos.Models;
using Lab.MVC.Data;

namespace Lab.MVC.CadastroClientesEProdutos.Controllers
{
    [Authorize] // Somente permite acesso a está página se estiver logado.

    public class PedidosController : Controller
    {

        private DB_VENDASEntities db = new DB_VENDASEntities();

        // GET: Pedidos Index
        public ActionResult Index()
        {
            return View();
        }


        // GET: Pedidos/Incluir Pedido
        public ActionResult Incluir()
        {
            ViewBag.ListaDeClientes = new SelectList(ClientesDao.ListarClientes(), "Documento", "Nome");
            return View();
        }

        // POST: Pedidos/Incluir
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Incluir([Bind(Include = "Id,DocCliente,Data,NumeroPedido")] Pedido pedido)
        public ActionResult Incluir(Pedido pedido)
        {
            //Omitiremos a data do pedido na view Incluir, porque no controller (AQUI) assumiremos a data atual.//
            pedido.Data = DateTime.Now; 

            if (!ModelState.IsValid)
            {
                return Incluir();
            }

            try
            {
                PedidosDao.IncluirPedido(pedido);

                ViewBag.ListaDeClientes = new SelectList(ClientesDao.ListarClientes(), "Documento", "Nome");
                return Redirect("/Pedidos/Listar?id=" + pedido.DocCliente);
            }
            catch (Exception ex)
            {
                ViewBag.MensagemErro = ex.Message;
                return View("Erro");
            }
        }

        // GET: Lista de Pedidos.
        public ActionResult Listar(string id)
        {
            try
            {
                ViewBag.ListaDeClientes = new SelectList(ClientesDao.ListarClientes(), "Documento", "Nome");
                return View(PedidosDao.ListarPedidos(id));
            }
            catch(Exception ex)
            {
                ViewBag.MensagemErro = ex.Message;
                return View("Erro");
            }
        }

        // GET: Pedidos/Delete/5
        public ActionResult Excluir(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Pedido pedido = db.Pedidos.Find(id);

            if (pedido == null)
            {
                return HttpNotFound();
            }
            return View(pedido);
        }

        // POST: Pedidos/Delete/5
        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirConfirmed(int id)
        {
            Pedido pedido = db.Pedidos.Find(id);
            db.Pedidos.Remove(pedido);
            db.SaveChanges();
            return RedirectToAction("Listar");
        }


        // GET: Pedidos/Details/5
        public ActionResult Detalhes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedidos.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            return View(pedido);
        }


        // GET: Pedidos/Edit/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedidos.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            ViewBag.DocCliente = new SelectList(db.Clientes, "Documento", "Nome", pedido.DocCliente);
            return View(pedido);
        }

        // POST: Pedidos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "Id,DocCliente,Data,NumeroPedido")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pedido).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DocCliente = new SelectList(db.Clientes, "Documento", "Nome", pedido.DocCliente);
            return View(pedido);
        }


        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
