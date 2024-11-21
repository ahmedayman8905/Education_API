using Api_1.Contract;
using Mapster;
using System.Data.Entity;
using System.Security.Cryptography;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Api_1.Outherize;

public class AuthService(EducationalPlatformContext _db, token token)
{
    public EducationalPlatformContext db = _db;
    public token Token = token;
    private readonly int RefreshTokenDays = 14;
    public StudentResponse Login(string Email, string password)
    {
        var user = db.Students.SingleOrDefault
            (i => i.Email == Email && i.Password == password);
        if (user == null)
        {
            return null;
        }
        var result = user.Adapt<StudentResponse>();
        var (token, expiresIn) = Token.GenerateToken(result);
        result.Token = token;
        result.ExpiresIn = expiresIn;

        result.RefreshToken = GenerateRefreshToken();
        result.RefreshTokenExpiretion = DateTime.UtcNow.AddDays(RefreshTokenDays);


        var addRefresh = db.RefreshTokens.SingleOrDefault(i => i.UserId == int.Parse(result.Id));
        if (addRefresh == null)
        {


            db.RefreshTokens.Add(new RefreshToken
            {
                UserId = int.Parse(result.Id),
                Id = int.Parse(result.Id) + 1,

                Token = result.RefreshToken,
                ExpiresOn = result.RefreshTokenExpiretion,

            });
        }
        else
        {
            addRefresh.Token = result.RefreshToken;
            addRefresh.ExpiresOn = result.RefreshTokenExpiretion;
        }
        db.SaveChanges();

        return result;
    }

    public StudentResponse GetRefreshTokenAsync(string tokenn, string refrehToken)
    {
        string userId = Token.ValisationToken(tokenn);
        if (userId is null) { return null; }
        var user =  db.Students.SingleOrDefault(i => i.Id == int.Parse(userId));
        if (user is null) { return null; }

        var UserRefreshToken =  db.RefreshTokens.SingleOrDefault
            (i => i.Token == refrehToken && i.RevokedOn == null );
        if (UserRefreshToken is null)
            return null;
       
         // *** to stop Refresh token
        // UserRefreshToken.RevokedOn = DateTime.UtcNow;

        var result = user.Adapt<StudentResponse>();
        var (newtoken, expiresIn) = Token.GenerateToken(result);
        result.Token = newtoken;
        result.ExpiresIn = expiresIn;

        result.RefreshToken = GenerateRefreshToken();
        result.RefreshTokenExpiretion = DateTime.UtcNow.AddDays(RefreshTokenDays);

        UserRefreshToken.Token = result.RefreshToken;
        UserRefreshToken.ExpiresOn = result.RefreshTokenExpiretion;
        db.SaveChanges();

        return result;
    }
       


    private static string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(count: 64));
    }
}
