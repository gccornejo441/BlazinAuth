using System;
using System.Collections.Generic;

namespace BlazorCookieAuthentication.WebApi.Entities;

public partial class Account
{
    public int Accountid { get; set; }

    public string Accountname { get; set; } = null!;

    public DateOnly Createddate { get; set; }

    public virtual ICollection<Maintainer> Maintainers { get; set; } = new List<Maintainer>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
