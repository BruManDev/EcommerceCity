
using Microsoft.AspNetCore.Mvc;
using ProjetoEcommerce.Models;
using ProjetoEcommerce.Repositorio;

namespace ProjetoEcommerce.Controllers
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
            return View(_produtoRepositorio.TodosProduto());
        }

        public IActionResult CadastrarProduto()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CadastrarProduto(Produto produto)
        {
            _produtoRepositorio.Cadastrar(produto);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult EditarProduto(int id)
        {
            var produto = _produtoRepositorio.ObterProduto(id);

            if (produto == null)
            {
                return NotFound();
            }
            return View(produto);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult EditarProduto(int id, [Bind("CodProd,Nome,Descricao,quantidade,preco")] Produto produto)
        {
            if (id != produto.CodProd)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (_produtoRepositorio.Atualizar(produto))
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao Editar");
                }
            }

            // Garante que sempre há um retorno
            return View(produto);
        }


        public IActionResult ExcluirProduto(int id)
        {
            _produtoRepositorio.Excluir(id);
            return RedirectToAction(nameof(Index));

        }

    }
}