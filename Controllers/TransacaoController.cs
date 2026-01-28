using APIFinanceira.Data;
using APIFinanceira.ViewModels;
using APIFinanceira.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;

namespace APIFinanceira.Controllers
{
    [ApiController]
    public class TransacaoController : ControllerBase
    {
        [HttpPost("v1/transacoes")]
        [Authorize]
        public async Task<IActionResult> CriarTransacaoAsync(
            [FromBody] EditorTransacaoViewModel model,
            [FromServices] ApiDataContext context)
        {
            var id = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            if(id == null)
                return StatusCode(401, new ResultViewModel<string>("Token invalido"));

            var usuarioId = int.Parse(id.Value);
            
            var transacao = new Models.Transacao
            {
                Valor = model.Valor,
                Tipo = (Transacao.TipoTransacao)model.Tipo,
                UsuarioId = usuarioId,
                Data = DateTime.Now
            };

            try
            {
                await context.Transacoes.AddAsync(transacao);
                await context.SaveChangesAsync();

                return Created($"v1/transacoes/{transacao.Id}", new ResultViewModel<Transacao>(transacao));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<string>("Falha interna no servidor"));
            }
        }

        [HttpGet("v1/transacoes")]
        [Authorize]
        public async Task<IActionResult> GetTrasancoesAsync(
            [FromServices] ApiDataContext context)
        {
            var id = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            if (id == null)
                return StatusCode(401, new ResultViewModel<string>("Token invalido"));

            var usuarioId = int.Parse(id.Value);

            var transacoes = await context.Transacoes
                .AsNoTracking()
                .Where(x => x.UsuarioId == usuarioId)
                .OrderByDescending(x => x.Data)
                .ToListAsync();

            return Ok(new ResultViewModel<List<Transacao>>(transacoes));
        }
    }
}
