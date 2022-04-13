using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab.MVC.CadastroClientesEProdutos.ViewModels
{
    public class ClientePedidoViewModel
    {
        public string Documento { get; set; }
        public string NomeCliente { get; set; }
        public string NumeroPedido { get; set; }
        public int IdPedido { get; set; }
    }
}