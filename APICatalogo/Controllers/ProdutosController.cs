using APICatalogo.Context;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : Controller
    {
        private readonly APICatalogoContext _context;

        public ProdutosController(APICatalogoContext contexto)
        {
            _context = contexto;
        }


    }
}
