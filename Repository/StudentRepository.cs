 using System.Web.Mvc;

namespace Api_1.Repository;

public class StudentRepository
{
    private static EducationalPlatformContext db = new EducationalPlatformContext();

    
    public IEnumerable<Student> all() => db.Students.ToList();

    public Student GetById(int id) => db.Students.SingleOrDefault(i => i.Id == id);
    public Student add (Student student)
    {
        db.Students.Add(student);
        db.SaveChanges();
        return student;
    }
    public bool update(int id , Student student)
    {
        var isfind = GetById(id);
        if (isfind is null)
            return false;
        isfind.FullName = student.FullName;
        db.SaveChanges();
        return true;
    }
    public bool Delete(int id)
    {
        var isfind = GetById(id);
        if (isfind is null)
            return false;
        db.Students.Remove(isfind);
        db.SaveChanges();
        return true;
    }

}
