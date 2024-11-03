using System;
using System.Collections.Generic;

namespace Api_1.Model;

public partial class Regestration
{
    public int StudentId { get; set; }

    public int CourseId { get; set; }

    public DateOnly? StarTdate { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
