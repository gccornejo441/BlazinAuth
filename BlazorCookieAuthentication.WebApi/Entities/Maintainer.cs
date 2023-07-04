using System;
using System.Collections.Generic;

namespace BlazorCookieAuthentication.WebApi.Entities;

public partial class Maintainer
{
    public int Maintainerid { get; set; }

    public string Maintainername { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int Accountid { get; set; }

    public virtual Account Account { get; set; } = null!;
}
