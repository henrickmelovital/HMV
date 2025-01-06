using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APICatalogo.Context;
using APICatalogo.Models;

namespace APICatalogo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly APICatalogoContext _context;

        public CategoriasController(APICatalogoContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("TodasCategorias")]
        public ActionResult<IEnumerable<CategoriaModel>> GetCategorias()
        {
            try
            {
                var categoria = _context.Categorias.AsNoTracking().Take(10).ToList();
                if (categoria is null)
                {
                    return NotFound("Categorias não encontrados... ");
                }
                return categoria;
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
        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<CategoriaModel> Get(int id)
        {
            try
            {
                var categoria = _context.Categorias.AsNoTracking().FirstOrDefault(p => p.CategoriaId == id);
                if (categoria is null)
                {
                    return NotFound("Categoria não encontrado... ");
                }
                return categoria;
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
        /// <returns></returns>
        [HttpGet("Produtos")]
        public ActionResult<IEnumerable<CategoriaModel>> GetCategoriaProduto()
        {
            try
            {
                var categoria = _context.Categorias.AsNoTracking().Include(x => x.Produtos).Take(10).ToList();
                if (categoria is null)
                {
                    return NotFound("Categoria não encontrados... ");
                }
                return categoria;
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
        /// <param name="categoria"></param>
        /// <returns></returns>
        [HttpPost("InserirCategorias")]
        public ActionResult Post(CategoriaModel categoria)
        {
            try
            {
                if (categoria is null)
                    return BadRequest();

                _context.Categorias.Add(categoria);
                _context.SaveChanges();

                return new CreatedAtRouteResult("ObterCategoria",
                    new { id = categoria.CategoriaId }, categoria);
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
        /// <param name="categoria"></param>
        /// <returns></returns>
        [HttpPut("{id:int}", Name = "AtualizaCategoria")]
        public ActionResult Put(int id, CategoriaModel categoria)
        {
            try
            {
                if (id != categoria.CategoriaId)
                {
                    return BadRequest();
                }
                _context.Entry(categoria).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok(categoria);
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
        [HttpDelete("{id:int}", Name = "DeletaCategoria")]
        public ActionResult Delete(int id)
        {
            try
            {
                var categoria = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);

                if (categoria == null)
                {
                    return NotFound("Categoria não encontrada...");
                }
                _context.Categorias.Remove(categoria);
                _context.SaveChanges();
                return Ok(categoria);
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
