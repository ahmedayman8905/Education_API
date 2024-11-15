using Api_1.Contract;
using Mapster;
using System.Data.Entity;

namespace Api_1.Outherize;

public class AuthService(EducationalPlatformContext _db , token token)
{
    public EducationalPlatformContext db = _db;
    public token Token  = token;

    public StudentResponse Login(string Email, string password)
    {
        var user =  db.Students.SingleOrDefault
            (i => i.Email == Email && i.Password == password);
        if (user == null)
        {
            return null;
        }
        var result = user.Adapt<StudentResponse>();
        var (token, expiresIn) = Token.GenerateToken(result);
        result.Token = token;
        result.ExpiresIn = expiresIn * 60;
        return result;
    }
}
