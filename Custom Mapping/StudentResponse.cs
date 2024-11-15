namespace Api_1.Contract;

public class StudentResponse
{
 
    public string Id { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }
    public string? Gender { get; set; }
    public string Token {  get; set; }
    public int ExpiresIn { get; set; }

}
