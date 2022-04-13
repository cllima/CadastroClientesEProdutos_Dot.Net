using Lab.MVC.CadastroClientesEProdutos.Models;
using Lab.MVC.CadastroClientesEProdutos.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Lab.MVC.CadastroClientesEProdutos.Data
{
    public class ItensDao
    {
        // Incluir Item.
        public static void IncluirItem(Item item)
        {
            using (var db = new DB_VENDASEntities())
            {
                db.Entry<Item>(item).State = EntityState.Added;
                db.SaveChanges();
            }
        }

        // Remover Item.
        public static void RemoverItem(Item item)
        {
            using(var db = new DB_VENDASEntities())
            {
                db.Entry<Item>(item).State = EntityState.Deleted;
                db.SaveChanges();

            }
        }

        // Buscar Item.
        public static Item BuscarItem(int id)
        {
            using(var db = new DB_VENDASEntities())
            {
                return db.Itens.FirstOrDefault(p => p.Id == id);
            }
        }

        // Cliente Pedidos.
        public static IEnumerable<ClientePedidoViewModel> ListarPedidos()
        {
            using (var db = new DB_VENDASEntities())
            {
                var lista = db.Clientes.Join(
                db.Pedidos,
                c => c.Documento, // c = Cliente
                p => p.DocCliente, // p = Pedido
                (c, p) => new ClientePedidoViewModel

                {
                    Documento = c.Documento,
                    NomeCliente = c.Nome + " - " + p.NumeroPedido,
                    IdPedido = p.Id,
                    NumeroPedido = p.NumeroPedido
                });
                return lista.ToList();
            }
        }

        // Itens Pedidos
        public static IEnumerable<ItensPedidoViewModel> ListarItensPorPedido(int? idPedido)
        {
            List<ItensPedidoViewModel> lista = new List<ItensPedidoViewModel>(); if (idPedido != null)
            {
                using (var db = new DB_VENDASEntities())
                {
                    lista = (from pedido in db.Pedidos
                             join item in db.Itens
                                on pedido.Id equals item.IdPedido
                                    join produto in db.Produtos
                                        on item.IdProduto equals produto.Id
                                            where pedido.Id == (int)idPedido

                             select new ItensPedidoViewModel

                             {
                                 IdItem = item.Id,
                                 QuantItens = item.Quantidade,
                                 IdPedido = pedido.Id,
                                 NumeroPedido = pedido.NumeroPedido,
                                 DescProduto = produto.Descricao,
                                 TotalItem = item.Quantidade *
                              (double)produto.Preco
                             }).ToList();
                }
            }
            return lista;
        }

        internal static IEnumerable ListarItensPorPedido()
        {
            throw new NotImplementedException();
        }
    }
}