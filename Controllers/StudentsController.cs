
using Api_1.Contract;
using Api_1.Entity;
using Api_1.Outherize;
using Api_1.Repository;
using Mapster;
using Microsoft.AspNetCore.Authorization;

namespace Api_1.Controllers;
[Route("api/[controller]")]
[ApiController]
public class StudentsController : ControllerBase
{
    private EducationalPlatformContext db;
    private StudentRepository student;
    private AuthService authService;
    public StudentsController(EducationalPlatformContext db, StudentRepository student, AuthService authService)
    {
        this.db = db;
        this.student = student;
        this.authService = authService;
    }

    // [HttpGet("/GetAllStudents")]
    [HttpGet]
    [Authorize]
    public async Task <IActionResult> Get()
    {
        // var result = db.Students.ToList();
        // return Ok(student.all().mapp_to_listStudenRespons());
        var result = await student.all();
     //   return Ok( result.Adapt<IEnumerable<StudentResponse>>()); 
     return Ok(result); 



    }

    [HttpGet]
    [Route("{id:int}")]
    public async  Task<IActionResult> GetById( int id)
    {
        //var result = db.Students.FirstOrDefault(s => s.Id == id);
        //return Ok(result);
        var result = await student.GetById(id);
        if (result is not null) 
            return Ok(result);
        return Content("notfound");


    }
    [HttpGet]
    [Route("{name:alpha}")]
    public IActionResult GetByName([FromRoute] string name)
    {
        var result = db.Students.FirstOrDefault(s => s.FullName == name);
        return Ok(result.mapp_to_student_Response());

    }
    [HttpPut]
    [Route("{Id}")]
    public async Task<IActionResult> updateStudent(int Id, Student newstudent , CancellationToken cancellationToken )
    {
        var isvalled = await student.update(Id, newstudent , cancellationToken);
        if (!isvalled)
            return NotFound();
        return CreatedAtAction(nameof(GetById) ,new { Id = newstudent.Id} , newstudent);


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
    public async Task<IActionResult> DeleteStudent(int id , CancellationToken cancellationToken = default)
    {
        var isvalled = await student.Delete(id , cancellationToken);
        if (!isvalled)
            return NotFound();
        return NoContent();

    }

    [HttpPost]
    public async Task<IActionResult> Add(Student newstudent, CancellationToken cancellationToken)
    {
        var result = await student.add(newstudent, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
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

    [HttpPost("/Login")]
    public  IActionResult Login(userLogin user)
    {
        // var isvalid = await student.Login(user.Email, user.Passsword);
        var isvalid =  authService.Login(user.Email, user.Passsword);
        if (isvalid is null)
            return BadRequest("Invalid email/password");
        return Ok(isvalid);

    }

    [HttpPost("/RefreshToken")]
    public IActionResult GetRefrehToken(RefreshtokenReqest request)
    {
        // var isvalid = await student.Login(user.Email, user.Passsword);
        var isvalid =  authService.GetRefreshTokenAsync(request.token , request.RefeshToken);
        if (isvalid is null)
            return BadRequest("Invalid");
        return Ok(isvalid);

    }

}
