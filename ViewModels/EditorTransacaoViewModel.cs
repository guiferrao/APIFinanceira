using Microsoft.OpenApi.MicrosoftExtensions;
using System.ComponentModel.DataAnnotations;

namespace APIFinanceira.ViewModels
{
    public class EditorTransacaoViewModel
    {
        [Required(ErrorMessage = "O valor é obrigatorio")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "O tipo é obrigatorio")]
        public int Tipo { get; set; }
    }
}
