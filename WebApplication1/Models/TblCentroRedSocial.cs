using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class TblCentroRedSocial
{
    public long Id { get; set; }

    public long? IdCentro { get; set; }

    public long? IdRedSocial { get; set; }

    public string? RedSocial { get; set; }

    public virtual TblCentro? IdCentroNavigation { get; set; }

    public virtual TblRedSocial? IdRedSocialNavigation { get; set; }
}
