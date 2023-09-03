using System.ComponentModel.DataAnnotations;

namespace ExemploAPI.Models.Request
{
	public class NovoAlunoViewModel	{
        [Required(ErrorMessage ="Nome é obrigatorio !")]
        [MinLength(2,ErrorMessage ="O nome deve ter no minimo 2 caracteres")]
        [MaxLength(50,ErrorMessage = "Maximo de caracteres é 50")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Nome é obrigatorio !")]
        [MinLength(2, ErrorMessage = "O RA deve ter no minimo 2 caracteres")]
        [MaxLength(6, ErrorMessage = "Maximo de caracteres é 6")]
        public string RA { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage ="CPF é obrigatorio")]
        public string CPF { get; set; }
        public bool Ativo { get; set; }
    }
}
