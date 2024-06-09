using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class TblPersonaDato
{
    public long Id { get; set; }

    public long? IdPersona { get; set; }

    public string? Dato { get; set; }

    public string? Texto { get; set; }

    public virtual TblPersona? IdPersonaNavigation { get; set; }
}
