using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class TblRedSocial
{
    public long IdRedSocial { get; set; }

    public string? RedSocial { get; set; }

    public virtual ICollection<TblCentroRedSocial> TblCentroRedSocials { get; set; } = new List<TblCentroRedSocial>();

    public virtual ICollection<TblPersonaRedSocial> TblPersonaRedSocials { get; set; } = new List<TblPersonaRedSocial>();
}
