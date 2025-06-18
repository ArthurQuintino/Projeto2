using Microsoft.AspNetCore.Mvc;
using Projeto2.Repositorio;

namespace Projeto2.Controllers
{
    public class ProdutoController : Controller
    {

        private readonly ProdutoRepositorio _produtoRepositorio;
        public ProdutoController(ProdutoRepositorio produtoRepositorio)
        {
            _produtoRepositorio = produtoRepositorio;
        }
        public IActionResult Index()
        {
            /* Retorna a View padrão associada a esta Action,
             passando como modelo a lista de todos os produtos obtido do repositório.*/
            return View(_produtoRepositorio.TodosProdutos());
        }

        

    }
}
