using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// Adicione o Entity Framework
using System.Data.Entity;

//Importar a pasta Models para sua Cliente Dao usa-la.
using Lab.MVC.CadastroClientesEProdutos.Models;


namespace Lab.MVC.Data
{
    public class ClientesDao
    {
        // vamos criar uma metodo static void ela não precisa ser  instânciada.
        public static void IncluirCliente(Cliente cliente)
        {
            // utilizamos o objeto using para criar uma instancia
            // do objeto de contexto do banco de dados
            // o using garante que este objeto não fique em memória
            // após seu uso
            using (var db = new DB_VENDASEntities())
            {

                //cria 
                db.Clientes.Add(cliente);
                // executar
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Método que faz a busca do cliente pelo Documento.
        /// </summary>
        /// <param name="documento">número do documento do cliente</param>
        /// <returns>Retorna o cliente com o número do documento informado.</returns>
        public static Cliente BuscarCliente(string documento)
        {

            using (var db = new DB_VENDASEntities())
            {
                return db.Clientes.FirstOrDefault(x => x.Documento.Equals(documento));
            }
        }

        /// <summary>
        /// Lista Clientes
        /// </summary>
        /// <returns>Lista de clientes</returns>
        public static IEnumerable<Cliente> ListarClientes()
        {

            using (var db = new DB_VENDASEntities())
            {
                return db.Clientes.ToList();
            }
        }

        /// <summary>
        /// Altera o cadastro do cliente
        /// </summary>
        /// <param name="cliente"></param>
        public static void AlterarCliente(Cliente cliente)
        {
            using (var db = new DB_VENDASEntities())
            {
                // para alterar um registro via entity
                // temos que colocar a tabela em estado de modificação
                db.Entry<Cliente>(cliente).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public static void RemoverCliente(Cliente cliente)
        {
            using (var db = new DB_VENDASEntities())
            {
                db.Entry<Cliente>(cliente).State = EntityState.Deleted;
                db.SaveChanges();
            }
        }
    }
}