using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace GameReviewApi.Middleware.CustomAuthorization
{
    public interface IHelperToken
    {
        int? ValidateToken(string token);
    }
    public class HelperToken : IHelperToken
    {
        private readonly IConfiguration _configuration;
        public HelperToken(IConfiguration configuration) => _configuration = configuration;
        public int? ValidateToken(string token)
        {
            if (token == null) 
            {
                return null;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JWT:SecretKey"]);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // установка clockskew равным нулю, чтобы срок действия токенов истекал точно в момент истечения срока действия токена.
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
                return userId;
            }
            catch
            {
                return null;
            }
        }
    }
}
