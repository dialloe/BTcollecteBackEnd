using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace BonDeCollecte.GenereToken.Services
{
    public interface ITokenService
    {
         string GenerateToken(string username, string role);
    }
}
