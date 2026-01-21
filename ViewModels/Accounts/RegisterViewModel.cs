using System.ComponentModel.DataAnnotations;

namespace APIFinanceira.ViewModels.Accounts
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "O nome é Obrigatorio")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O email é Obrigatorio")]
        [EmailAddress(ErrorMessage = "O email é invalido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é Obrigatoria")]
        [MinLength(6, ErrorMessage = "A senha deve ter no minimo 6 caracteres")]
        public string Senha { get; set; }
    }
}
