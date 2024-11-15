using Api_1.Contract;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api_1.Outherize;

public class token
{
    public (string token, int expiresIn) GenerateToken(StudentResponse user)
    {
        // Define claims for the token
        Claim[] claims = new[]
        {
           
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Gender, user.Gender),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Name, user.Name),
            new Claim(JwtRegisteredClaimNames.GivenName, user.Password),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        // Create the key for signing the token
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("J7MfAb4WcAIMkkigVtIepIILOVJEjacB"));

        // Define signing credentials with security algorithm
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);


        //
        var expiresIn = 30;

        var token = new JwtSecurityToken(
            issuer: "SurveyBasketApp",
            audience: "SurveyBasketApp users",
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expiresIn),
            signingCredentials: signingCredentials
        );

        return (token: new JwtSecurityTokenHandler().WriteToken(token), expiresIn: expiresIn);




        //// Calculate token expiration time
        //var expirationDate = DateTime.UtcNow.AddMinutes(expiresIn);

        //// Create the token descriptor
        //var tokenDescriptor = new SecurityTokenDescriptor
        //{
        //    Subject = new ClaimsIdentity(claims),
        //    Expires = expirationDate,
        //    SigningCredentials = signingCredentials
        //};

        //// Generate the token
        //var tokenHandler = new JwtSecurityTokenHandler();
        //var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        //var token = tokenHandler.WriteToken(securityToken);

        //return (token, expiresIn);
    }

}
