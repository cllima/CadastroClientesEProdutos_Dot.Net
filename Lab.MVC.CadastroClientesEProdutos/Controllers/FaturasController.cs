using Lab.MVC.CadastroClientesEProdutos.Data;
using Lab.MVC.CadastroClientesEProdutos.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Lab.MVC.CadastroClientesEProdutos.Controllers
{
    public class FaturasController : Controller
    {
        HttpClient client;

        public FaturasController()
        {
            if (client == null)
            {
                client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:44362/");
                client.DefaultRequestHeaders.Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }
        }


        // GET: Faturas
        public ActionResult Index()
        {
            return View();
        }

        // Efetuar pagto.
        [HttpGet]
        public ActionResult EfetuarPagamento()
        {
            try
            {
                ViewBag.ListaDePedidos =
                    new SelectList(ItensDao.ListarPedidos(), "NumeroPedido", "NomeCliente");

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.MensagemErro = ex.Message;
                return View("Erro");
            }
        }

        // Post Faturas
        [HttpPost]
        public async Task<ActionResult> EfetuarPagamento(Fatura fatura)
        {
            try
            {
                //obtendo id do pedido
                int idPedido = PedidosDao.BuscarPorPedido(fatura.NumeroPedido).Id;

                //obtendo a soma dos itens do pedido
                double totalPedido =
                    ItensDao.ListarItensPorPedido(idPedido).ToList().Sum(p => p.TotalItem);

                //completando o objeto Fatura
                fatura.Valor = totalPedido;
                fatura.Status = 1;

                //obtendo o conteúdo JSON a partir do objeto
                string json = JsonConvert.SerializeObject(fatura);

                //serializando o objeto
                HttpContent content = new StringContent(json, System.Text.Encoding.Unicode, "application/json");

                //enviando o conteúdo serializado para o serviço
                var response = await client.PostAsync("api/pagamentos", content);

                if (response.IsSuccessStatusCode)
                {
                    return View("_Sucesso");
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                ViewBag.MensagemErro = ex.Message;
                return View("Erro");
            }
        }


        // Listar Fatura.
        public async Task<ActionResult> ListarFaturas()
        {
            try
            {
                HttpResponseMessage response = client.GetAsync("api/pagamentos").Result;
                if (response.IsSuccessStatusCode)
                {
                    var resultado = await response.Content.ReadAsStringAsync();
                    var lista = JsonConvert.DeserializeObject<Fatura[]>(resultado).ToList();
                    return View(lista);
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                ViewBag.MensagemErro = ex.Message;
                return View("Erro");
            }
        }


    }
}