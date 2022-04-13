using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab.MVC.CadastroClientesEProdutos.ViewModels
{
    public class ItensPedidoViewModel
    {
        public int IdItem { get; set; }
        public double QuantItens { get; set; }
        public double TotalItem { get; set; }
        public string DescProduto { get; set; }
        public int IdPedido { get; set; }
        public string NumeroPedido { get; set; }
    }
}