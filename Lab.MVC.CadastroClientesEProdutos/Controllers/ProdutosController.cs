using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Lab.MVC.CadastroClientesEProdutos.Models;
using Lab.MVC.CadastroClientesEProdutos.Data;

namespace Lab.MVC.CadastroClientesEProdutos.Controllers
{
    public class ProdutosController : Controller
    {
        // GET: Produtos
        public ActionResult Index()
        {
            return View();
        }


        // GET: Produtos/Incluir
        public ActionResult Incluir()
        {
            ViewBag.ListaDeCategorias = new SelectList(ProdutosDao.ListarCategorias(), "Id", "Descricao");

            return View();
        }

        // POST: Produtos/Incluir
        [HttpPost]
        [Authorize] // Somente permite acesso se estiver logado.
        public ActionResult Incluir(Produto produto, HttpPostedFileBase image)
        {
            // Coloca aqui a validação do estado do objeto do produto.
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(String.Empty, "Verifique os dados do Produto.");
                return View();  
            }


            try
            {
                // TODO: Adicione lógica de inserção aqui.

                // Vamos fazer o tratamento de upload de imagens.
                if (image != null)
                {
                    // Configurando a rotina para transformar em bite esta imagem.
                    produto.MimeType = image.ContentType;
                    byte[] bytesImage = new byte[image.ContentLength];
                    
                    // Convertendo a imagem em bytes[].
                    image.InputStream.Read(bytesImage, 0, image.ContentLength);

                    // Agora basta atribuir o conteúdo em bytes[] para ser a propriedades da classe.
                    produto.Foto = bytesImage;
                }

                else
                {
                    throw new Exception("Não foi possivel salvar a imagem, verifique!!");
                }

                // Então mandamos inserir no Banco de dados.
                ProdutosDao.IncluirProduto(produto);

                return RedirectToAction("Listar");
            }
            catch(Exception ex)
            {
                ViewBag.MensagemErro = ex.Message;
                return View();
            }
        }


        // Método Listar:
        public ActionResult Listar()
        {
            return View(ProdutosDao.ListaProduto());  
        }


        // Método Buscar Foto
        public FileResult BuscarFoto(int id)
        {
            var produto = ProdutosDao.BuscarProduto(id);

            return File(produto.Foto, produto.MimeType);

        }


       
        // GET: Produtos/Editar/5
        public ActionResult Editar(int? id)
        {
            // Fazer um Try e Catch
            try
            {
                if (id == null)
                {
                    ModelState.AddModelError(string.Empty, "O código do produto é Invalido.");
                    return RedirectToAction("Listar");
                }

                // Temos que fazer um CAST (conversão)  de int? para int.
                var produto = ProdutosDao.BuscarProduto((int)id);

                if (produto == null)
                {
                    throw new Exception("Nenhum produto foi localizado com o código informado.");
                }

                // Devolvemos um Select List para mostrar na View um Dropdownlist.
                ViewBag.ListaCategorias = new SelectList(ProdutosDao.ListarCategorias(), "Id", "Descricao");

                return View(produto);
            }
            catch (Exception ex)
            {

                ViewBag.MensagemErro = ex.Message;
                return View("Erro");
            }
        }

        // POST: Produtos/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken] // colocar
        [Authorize] // Somente permite acesso se estiver logado.
        public ActionResult Editar(Produto produto, HttpPostedFileBase image)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Por favor conferir os dados do Cadastro."); 
                return View();
            }

            try
            {
                TransformarImagemEmArrayBytes(produto, image);

                // Então mandamos Alterar no Banco de dados.
                ProdutosDao.AlterarProduto(produto);

                return RedirectToAction("Listar");
            }
            catch (Exception ex)
            {
                ViewBag.MensagemErro = ex.Message;
                return View("Erro");
            }
        }

        private static void TransformarImagemEmArrayBytes(Produto produto, HttpPostedFileBase image)
        {
            // Vamos fazer o tratamento de upload de imagens.
            if (image != null)
            {
                // Configurando a rotina para transformar em bite esta imagem.
                produto.MimeType = image.ContentType;
                byte[] bytesImage = new byte[image.ContentLength];

                // Convertendo a imagem em bytes[].
                image.InputStream.Read(bytesImage, 0, image.ContentLength);

                // Agora basta atribuir o conteúdo em bytes[] para ser a propriedades da classe.
                produto.Foto = bytesImage;
            }

            else
            {
                throw new Exception("Não foi possivel salvar a imagem, verifique!!");
            }
        }


        // GET: Produtos/Detalhes/5
        public ActionResult Detalhes(int? id)
        {
            if (id == null)
            {
                return View();
            }

            var produto = ProdutosDao.BuscarProduto((int)id);

            if (produto == null)
            {
                ModelState.AddModelError(string.Empty, "Produto não localizado.");
            }

            return View(produto);
        }


        // Falta Finalizar 17/03/22
        // GET: Produtos/Excluir/5
        public ActionResult Excluir(int? id)
        {

            if (id == null)
            {
                return View();
            }

            var produto = ProdutosDao.BuscarProduto((int)id);

            if (produto == null)
            {
                ModelState.AddModelError(string.Empty, "Produto não encontrado.");
            }

            return View(produto);
        }

        // POST: Produtos/Excluir/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize] // Somente permite acesso se estiver logado.
        public ActionResult Excluir(Produto produto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(produto.Descricao)) // Verificar Descrição
                {
                    ModelState.AddModelError(string.Empty, "Não foi possível excluir o registro");
                    return View();
                }

                produto = ProdutosDao.BuscarProduto(produto.Id);

                if (produto == null)
                {
                    ModelState.AddModelError(string.Empty, "Não foi possível excluir o registro");
                    return View();
                }

                // remove o Produto.
                ProdutosDao.RemoverProduto(produto);
                //Produto.SaveChanges(); // Verificar 



                return RedirectToAction("Listar");
            }
            catch (Exception ex)
            {
                ViewBag.MensagemErro = ex.Message;
                return View("Erro");
            }

        }
    }
}

