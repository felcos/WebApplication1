using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class TblTipoCentro
{
    public long IdTipoCentro { get; set; }

    public string? TipoCentro { get; set; }

    public virtual ICollection<TblCentro> TblCentros { get; set; } = new List<TblCentro>();
}
