﻿using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly APICatalogoContext _context;

        public ProdutosController(APICatalogoContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<ProdutoModel>> GetProdutos()
        {
            try
            {
                var produtos = _context.Produtos.AsNoTracking().ToList();
                if (produtos is null)
                {
                    return NotFound("Produtos não encontrados... ");
                }
                return produtos;
            }
            catch (InvalidOperationException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema ao processar a solicitação. Tente novamente mais tarde.");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro inesperado. Por favor, tente novamente mais tarde.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}", Name="ObterProduto")]
        public ActionResult<ProdutoModel> Get(int id)
        {
            try
            {
                var produto = _context.Produtos.AsNoTracking().FirstOrDefault(p => p.ProdutoId == id);
                if (produto is null)
                {
                    return NotFound("Produto não encontrado... ");
                }
                return produto;
            }
            catch (InvalidOperationException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema ao processar a solicitação. Tente novamente mais tarde.");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro inesperado. Por favor, tente novamente mais tarde.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="produto"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Post(ProdutoModel produto)
        {
            try
            {
                if (produto is null)
                {
                    return BadRequest();
                }
                _context.Produtos.Add(produto);
                _context.SaveChanges();

                return new CreatedAtRouteResult("obterProduto", new { id = produto.ProdutoId }, produto);

            }
            catch (InvalidOperationException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema ao processar a solicitação. Tente novamente mais tarde.");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro inesperado. Por favor, tente novamente mais tarde.");
            }

        }

        /// <summary>
        ///  Um serviço com o verbo Put atualiza todas as informações.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="produto"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        public ActionResult Put(int id, ProdutoModel produto)
        {
            try
            {
                if (id != produto.ProdutoId)
                {
                    return BadRequest();
                }

                _context.Entry(produto).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();

                return Ok(produto);
            }
            catch (InvalidOperationException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema ao processar a solicitação. Tente novamente mais tarde.");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro inesperado. Por favor, tente novamente mais tarde.");
            }   
        }

        /// <summary>
        ///  Um serviço com o verbo Patch permite que atualize apenas uma informação de cada vez
        /// </summary>
        /// <param name="id"></param>
        /// <param name="produto"></param>
        /// <returns></returns>
        [HttpPatch("{id:int}")]
        public ActionResult Patch(int id, ProdutoModel produto)
        {
            try
            {
                if (id != produto.ProdutoId)
                {
                    return BadRequest();
                }

                var existingProduto = _context.Produtos.Find(id);
                if (existingProduto == null)
                {
                    return NotFound();
                }

                _context.Entry(existingProduto).CurrentValues.SetValues(produto);
                _context.SaveChanges();

                return Ok(existingProduto);
            }
            catch (InvalidOperationException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema ao processar a solicitação. Tente novamente mais tarde.");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro inesperado. Por favor, tente novamente mais tarde.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            try
            {
                var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);

                if (produto is null)
                {
                    return NotFound("Produto não localizado...");
                }

                _context.Produtos.Remove(produto);
                _context.SaveChanges();

                return Ok(produto);
            }
            catch (InvalidOperationException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema ao processar a solicitação. Tente novamente mais tarde.");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro inesperado. Por favor, tente novamente mais tarde.");
            }

        }
    }
}
