using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// EF 6
using System.Data.Entity;
// incluir uma ref. para pasta models.
using Lab.MVC.CadastroClientesEProdutos.Models;

namespace Lab.MVC.CadastroClientesEProdutos.Data
{
    public class ProdutosDao
    {
        // Incluir Produto
        public static void IncluirProduto(Produto produto)
        {
            using (var db = new DB_VENDASEntities())
            {
                db.Produtos.Add(produto);

                db.SaveChanges();
            }
        }

        //Buscar Produto.
        public static Produto BuscarProduto(int id)
        {
            using (var db = new DB_VENDASEntities())
            {
                return db.Produtos.FirstOrDefault(p => p.Id == id);
            }
        }

        // Listar Produto
        public static IEnumerable<Produto> ListaProduto()
        {
            using (var db = new DB_VENDASEntities())
            {
                return db.Produtos.ToList();
            }
        }

        // Alterar Produto.
        public static void AlterarProduto(Produto produto)
        {
            using (var db = new DB_VENDASEntities())
            {
                // Para fazer a Alteração, temos que mudar o Estado da Tabela.
                db.Entry<Produto>(produto).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        // Listar Categoria.
        public static IEnumerable<Categoria> ListarCategorias()
        {
            using (var db = new DB_VENDASEntities())
            {
                return db.Categorias.ToList();
            }
        }

        public static void RemoverProduto(Produto produto)
        {
            using (var db = new DB_VENDASEntities())
            {
                db.Entry<Produto>(produto).State = EntityState.Deleted;
                db.SaveChanges();
            }
        }
    }
}