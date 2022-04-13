using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
 

namespace Lab.MVC.CadastroClientesEProdutos.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name ="Usuário")]
        public string Nome { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}