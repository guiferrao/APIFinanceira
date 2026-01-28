using APIFinanceira.Data;
using APIFinanceira.Models;
using APIFinanceira.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace APIFinanceira.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet("v1/home")]
        [Authorize]
        public async Task<IActionResult> GetHomeAsync(
            [FromServices] ApiDataContext context)
        {
            var id = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            if(id == null)
                return StatusCode(401, new ResultViewModel<string>("faça login novamente"));

            var usuarioId = int.Parse(id.Value);

            var transacoes = await context.Transacoes
                .AsNoTracking()
                .Where(x => x.UsuarioId == usuarioId)
                .ToListAsync();

            var entradas = transacoes
                .Where(x => x.Tipo == Transacao.TipoTransacao.Entrada)
                .Sum(x => x.Valor);

            var saidas = transacoes
                .Where(x => x.Tipo == Transacao.TipoTransacao.Saida)
                .Sum(x => x.Valor);

            var home = new HomeViewModel
            {
                Entradas = entradas,
                Saidas = saidas,
                Saldo = entradas - saidas
            };

            return Ok(new ResultViewModel<HomeViewModel>(home));
        }
    }
}
