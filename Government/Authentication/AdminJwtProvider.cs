
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace Government.Authentication
{
    public class AdminJwtProvider : IAdminJwtProvider
    {

        private readonly JwtOption _options;

        public AdminJwtProvider(IOptions<JwtOption> options)
        {
            _options = options.Value;
        }
        public (string token, int expiresIn) GenerateToken(AppUser user, IEnumerable<string> roles, IEnumerable<string> permissions)
        {
            Claim[] claims = [ // Claim -->class in .net --> key-value paire
            new(JwtRegisteredClaimNames.Sub, user.Id), // JwtRegisteredClaimNames --->> static class
            new(JwtRegisteredClaimNames.Email, user.Email!),
            new(JwtRegisteredClaimNames.GivenName, user.FirstName),
            new(JwtRegisteredClaimNames.FamilyName, user.LastName),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new (nameof(roles),JsonSerializer.Serialize(roles),JsonClaimValueTypes.JsonArray),//string.Join(",", Roles)
            new (nameof(permissions),JsonSerializer.Serialize(permissions),JsonClaimValueTypes.JsonArray)
        ];


            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));

            var singingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            //
            var expiresMin = _options.ExpiryMinutes;

            var token = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiresMin),
                signingCredentials: singingCredentials
            );

            return (token: new JwtSecurityTokenHandler().WriteToken(token), expireIn: expiresMin * 60);
        }


    }
}
