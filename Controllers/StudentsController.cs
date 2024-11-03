
using Api_1.Repository;

namespace Api_1.Controllers;
[Route("api/[controller]")]
[ApiController]
public class StudentsController : ControllerBase
{
    private EducationalPlatformContext db;
    private StudentRepository student;

    public StudentsController(EducationalPlatformContext db, StudentRepository student)
    {
        this.db = db;
        this.student = student;
    }

    [HttpGet("/GetAllStudents")]
    public IActionResult Get()
    {
        // var result = db.Students.ToList();
        
        return Ok(student.all());

    }

    [HttpGet]
    [Route("{id:int}")]
    public IActionResult GetById( int id)
    {
        //var result = db.Students.FirstOrDefault(s => s.Id == id);
        //return Ok(result);
        return Ok(student.GetById(id));


    }
    [HttpGet]
    [Route("{name:alpha}")]
    public IActionResult GetByName([FromRoute] string name)
    {
        var result = db.Students.FirstOrDefault(s => s.FullName == name);
        return Ok(result);

    }
    [HttpPut]
    [Route("{Id}")]
    public IActionResult PutStudent(int Id, Student newstudent)
    {
        var isvalled = student.update(Id, newstudent);
        if (!isvalled)
            return NotFound();
        return NoContent();
        
    }


    //[HttpPut]
    //[Route("{Id}")]
    //public IActionResult PutStudent([FromRoute] int Id, [FromBody] Student student)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        var item = db.Students.FirstOrDefault(i => i.Id == Id);
    //        if (item != null)
    //        {
    //            item.FullName = student.FullName;
    //            db.SaveChanges();
    //            return StatusCode(200);
    //        }
    //    }
    //    return BadRequest();

    //}

    [HttpDelete("{id}")]
    public IActionResult DeleteStudent(int id)
    {
        var isvalled = student.Delete(id);
        if (!isvalled)
            return NotFound();
        return NoContent();

    }

    [HttpPost]
    public IActionResult Add(Student newstudent)
    {
        var result =  student.add(newstudent);
        return CreatedAtAction(nameof(GetById) , new { id = result.Id} , result);
    }

    //[HttpPost]
    //public IActionResult Add(Student student)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        db.Students.Add(student);
    //        db.SaveChanges();

    //        string lin = Url.Link("GetStudentById", new { id = student.Id });
    //        return Created(lin, student);
    //        //return Created("http://localhost:50525/api/Student/" + student.Id , student);
    //    }
    //    return BadRequest(ModelState);
    //}

}
