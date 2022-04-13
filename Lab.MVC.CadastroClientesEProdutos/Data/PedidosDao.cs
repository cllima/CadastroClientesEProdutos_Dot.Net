using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Lab.MVC.CadastroClientesEProdutos.Models;

namespace Lab.MVC.CadastroClientesEProdutos.Data
{
    public class PedidosDao
    {
        // Método para Incluir um novo Pedido.
        public static void IncluirPedido(Pedido pedido)
        {
            using(var db = new DB_VENDASEntities())
            { 
                db.Pedidos.Add(pedido);
                db.SaveChanges();
            }
        }

        //método para listar os pedidos
        public static IEnumerable<Pedido> ListarPedidos(string Doc)
        {
            using (var db = new DB_VENDASEntities())
            {
                var lista = db.Pedidos.ToList();

                if (!string.IsNullOrEmpty(Doc))
                {
                    lista = lista.Where(p => p.DocCliente.Equals(Doc)).ToList();
                }
                return lista;
            }
        }

        //método para BuscarPedido por Id
        public static Pedido BuscarPedido(int id)
        {
            using(var db = new DB_VENDASEntities())
            {
                return db.Pedidos.FirstOrDefault(p => p.Id == id);
            }
        }

        //método para BuscarPorPedido
        public static Pedido BuscarPorPedido(string numeroPedido)
        {
            using (var db = new DB_VENDASEntities())
            {
                return db.Pedidos.FirstOrDefault(p => p.NumeroPedido == numeroPedido);
            }
        }

        //método para remover/excluir um pedido
        public static void RemoverPedido(Pedido pedido)
        {
            using(var db = new DB_VENDASEntities())
            {
                db.Entry<Pedido>(pedido).State = EntityState.Deleted;
                db.SaveChanges();
            }
        }
    }
}