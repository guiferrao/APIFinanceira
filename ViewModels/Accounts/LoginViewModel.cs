using System.ComponentModel.DataAnnotations;

namespace APIFinanceira.ViewModels.Accounts
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O Email é obrigatório")]
        [EmailAddress(ErrorMessage = "O email é inválido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "A senha é obrigatória")]
        public string Senha { get; set; }
    }
}
