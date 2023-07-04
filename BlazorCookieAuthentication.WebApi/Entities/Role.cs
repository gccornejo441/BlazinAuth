using System;
using System.Collections.Generic;

namespace BlazorCookieAuthentication.WebApi.Entities;

public partial class Role
{
    public int Roleid { get; set; }

    public string Rolename { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
