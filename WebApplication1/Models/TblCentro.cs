using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class TblCentro
{
    public long IdCentro { get; set; }

    public long IdTipoCentro { get; set; }

    public string? Nombre { get; set; }

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public string? Email { get; set; }

    public string? Web { get; set; }

    public virtual TblTipoCentro IdTipoCentroNavigation { get; set; } = null!;

    public virtual ICollection<TblCentroDato> TblCentroDatos { get; set; } = new List<TblCentroDato>();

    public virtual ICollection<TblCentroRedSocial> TblCentroRedSocials { get; set; } = new List<TblCentroRedSocial>();
}

