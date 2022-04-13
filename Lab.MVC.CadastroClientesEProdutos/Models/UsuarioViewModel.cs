using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// Data Annotations
using System.ComponentModel.DataAnnotations;

// Criando Usuário

namespace Lab.MVC.CadastroClientesEProdutos.Models
{
    public class UsuarioViewModel
    {
        [Required]
        [Display(Name ="Usuário")]
        public string Nome { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Compare("Senha")] // Validar a Senha
        [DataType(DataType.Password)]
        [Display(Name ="Confirma Senha")]
        public string Confirma { get; set; }

    }
}