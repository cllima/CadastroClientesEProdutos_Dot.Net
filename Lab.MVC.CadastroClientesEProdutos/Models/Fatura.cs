using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace Lab.MVC.CadastroClientesEProdutos.Models
{
    public class Fatura
    {
        public int Id { get; set; }

        [Display(Name ="Cartão")]
        public string NumeroCartao { get; set; }

        [Display(Name ="Pedido")]
        public string NumeroPedido { get; set; }

        [DataType(DataType.Currency)]
        public double Valor { get; set; }
        public int Status { get; set; }
    }
}