﻿using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APICatalogo.Models
{
    [Table("Categorias")]
    public class CategoriaModel
    {
        public CategoriaModel()
        {
            Produtos = new Collection<ProdutoModel>();
        }

        [Key]
        public int CategoriaId { get; set; }

        [Required]
        [StringLength(80)]
        public string? Nome { get; set; }

        [Required]
        [StringLength(300)]
        public string? ImagemUrl { get; set; }

        /// <summary>
        /// Relacionamentos
        /// </summary>
        public ICollection<ProdutoModel>? Produtos { get; set; }
    }
}
