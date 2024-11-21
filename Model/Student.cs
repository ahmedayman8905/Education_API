using System;
using System.Collections.Generic;

namespace Api_1.Model;

public partial class Student
{
    public int Id { get; set; }

    public string? FullName { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public DateTime? JoinDate { get; set; }

    public string? Password { get; set; }

    public string? Gender { get; set; }

    public DateOnly? BirthDay { get; set; }

    public string? IsDelete { get; set; }

    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

    public virtual ICollection<Regestration> Regestrations { get; set; } = new List<Regestration>();
}
