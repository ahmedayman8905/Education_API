using System;
using System.Collections.Generic;

namespace Api_1.Model;

public partial class RefreshToken
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Token { get; set; } = null!;

    public DateTime ExpiresOn { get; set; }

    public DateTime? CreatedOn { get; set; } = DateTime.UtcNow;

    public DateTime? RevokedOn { get; set; }

    public virtual Student User { get; set; } = null!;

    public bool IsExpired => DateTime.UtcNow >= ExpiresOn;

    public bool IsActive => RevokedOn is null && IsExpired == false;

}
