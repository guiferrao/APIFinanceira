using APIFinanceira.Data;
using APIFinanceira.Extensions;
using APIFinanceira.Models;
using APIFinanceira.Services;
using APIFinanceira.ViewModels;
using APIFinanceira.ViewModels.Accounts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;

namespace APIFinanceira.Controllers
{
    [ApiController]
    public class AccountControllers : ControllerBase
    {
        [HttpPost("v1/account/register")]
        public async Task<IActionResult> RegisterAsync(
            [FromBody] RegisterViewModel model,
            [FromServices] ApiDataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

            var usuario = new Usuario
            {
                Nome = model.Nome,
                Email = model.Email,
                SenhaHash = PasswordHasher.Hash(model.Senha)
            };

            try
            {
                await context.Usuarios.AddAsync(usuario);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<string>("Usuário cadastrado com sucesso"));
            }
            catch (DbUpdateException)
            {
                return StatusCode(400, new ResultViewModel<string>("Este e-mail já está cadastrado"));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<string>("Falha interna no servidor"));
            }
        }

        [HttpPost("v1/account/login")]
        public async Task<IActionResult> LoginAsync(
            [FromBody] LoginViewModel model,
            [FromServices] ApiDataContext context,
            [FromServices] TokenService tokenService)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

            var usuario = await context.Usuarios
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == model.Email);

            if (usuario == null)
                return StatusCode(401, new ResultViewModel<string>("Usuário ou senha inválidos"));

            if (!PasswordHasher.Verify(usuario.SenhaHash, model.Senha))
                return StatusCode(401, new ResultViewModel<string>("Usuário ou senha inválidos"));

            try
            {
                var token = tokenService.GenerateToken(usuario);
                return Ok(new ResultViewModel<string>(token));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<string>("Falha interna no servidor"));
            }
        }
    }
}
