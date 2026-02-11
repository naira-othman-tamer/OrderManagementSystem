namespace OrderManagementSystem.Application.Settings
{
    public static class JwtTokenGenerator
    {
        public static string GenerateToken(Guid userId, Role role, string userName, JwtSettings jwtSettings, int? customerId = null)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey));

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDiscriptor = new SecurityTokenDescriptor
            {
                Issuer = jwtSettings.Issuer,
                Audience = jwtSettings.Audience,
                Expires = DateTime.Now.AddMinutes(jwtSettings.ExpirationInMinutes),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.NameIdentifier , userId.ToString()),
                    new Claim (ClaimTypes.Role, role.ToString())
                }.Concat(customerId.HasValue ? new[]
                {
                    new Claim(ClaimTypes.Sid, customerId.Value.ToString())
                } : Array.Empty<Claim>()))


            };

            var token = tokenHandler.CreateToken(tokenDiscriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
