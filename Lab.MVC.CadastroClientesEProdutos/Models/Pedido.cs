
namespace Lab.MVC.CadastroClientesEProdutos.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Pedido
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Pedido()
        {
            this.Itens = new HashSet<Item>();
        }
    
        public int Id { get; set; }

        [Required]
        [Display(Name = "Cliente")]
        public string DocCliente { get; set; }

        [Display(Name = "Data do Pedido")]
        [DataType(DataType.Date)]
        public System.DateTime Data { get; set; }

        [Required]
        [Display(Name = "N. do Pedido")]
        public string NumeroPedido { get; set; }
    
        public virtual Cliente ClienteInfo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Item> Itens { get; set; }
    }
}
