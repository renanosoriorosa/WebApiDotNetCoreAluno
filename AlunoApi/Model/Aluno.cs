using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlunoApi.Model
{
    public class Aluno
    {
        public int Id { get; set; }

        [Required]
        [StringLength(80)]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        public int Idade { get; set; }
    }
}
