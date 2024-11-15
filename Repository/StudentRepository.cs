using Microsoft.EntityFrameworkCore;
using System.Web.Mvc;

namespace Api_1.Repository;

public class StudentRepository(EducationalPlatformContext _db)
{
    private readonly EducationalPlatformContext db = _db;

    
    public async Task<IEnumerable<Student>> all() => await db.Students.Where(i => i.IsDelete == "foles").ToListAsync();

    public async Task <Student> GetById(int id) => await db.Students.SingleOrDefaultAsync(i => i.Id == id);
    
    public async Task <Student> add (Student student , CancellationToken cancellationToken = default)
    {
        await db.Students.AddAsync(student , cancellationToken);
        await db.SaveChangesAsync(cancellationToken);
        return student;
    }   
   
    public async Task <bool> update(int id , Student student, CancellationToken cancellationToken = default)
    {
        var isfind = await GetById(id);
        if (isfind is null)
            return false;
        isfind.FullName = student.FullName;
        await db.SaveChangesAsync(cancellationToken);
        return  true;
    }
    public async Task <bool> Delete(int id , CancellationToken cancellationToken = default)
    {
        var isfind = await GetById(id);
        if (isfind is null)
            return false;
        //db.Students.Remove(isfind);
        isfind.IsDelete = "true";
        await db.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<Student> Login (string Email, string password)
    {
        var user = await db.Students.SingleOrDefaultAsync
            (i=> i.Email == Email && i.Password == password );
        return user;
    }

}
