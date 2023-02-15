using System;
using System.Collections.Generic;

namespace SupplyApp.Entity;

public partial class User
{
    public long Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public long RoleId { get; set; }

    public virtual Role Role { get; set; } = null!;
}
