using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Context
{
    public class APICatalogoContext : DbContext
    {
        public APICatalogoContext(DbContextOptions<APICatalogoContext> options) : base(options)
        {

        }

        public DbSet<ProdutoModel> Produtos { get; set; }
        public DbSet<CategoriaModel> Categorias { get; set; }
    }
}
