//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Lab.MVC.CadastroClientesEProdutos.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Item
    {
        public int Id { get; set; }

        [Required]
        [Display(Name ="Cliente e N. Pedido")]
        public int IdPedido { get; set; }

        [Required]
        [Display(Name ="Produto")]
        public int IdProduto { get; set; }

        [Required]
        public double Quantidade { get; set; }
    

        public virtual Pedido PedidoInfo { get; set; }
        public virtual Produto ProdutoInfo { get; set; }
    }
}