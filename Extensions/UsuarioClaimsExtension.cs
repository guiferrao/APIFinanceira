using APIFinanceira.Models;
using System.Security.Claims;

namespace APIFinanceira.Extensions
{
    public static class UsuarioClaimsExtension
    {
        public static IEnumerable<Claim> GetClaims(this Usuario usuario)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim(ClaimTypes.Email, usuario.Email)
            };
            return claims;
        }
    }
}
