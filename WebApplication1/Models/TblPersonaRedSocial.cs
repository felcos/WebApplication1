using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class TblPersonaRedSocial
{
    public long Id { get; set; }

    public long? IdRedSocial { get; set; }

    public long? IdPersona { get; set; }

    public string? RedSocial { get; set; }

    public virtual TblPersona? IdPersonaNavigation { get; set; }

    public virtual TblRedSocial? IdRedSocialNavigation { get; set; }
}
