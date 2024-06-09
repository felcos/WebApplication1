using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class TblCentroDato
{
    public long Id { get; set; }

    public long? IdCentro { get; set; }

    public string? Dato { get; set; }

    public string? Texto { get; set; }

    public virtual TblCentro? IdCentroNavigation { get; set; }
}
