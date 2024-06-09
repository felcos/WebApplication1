using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class ViewCentro
{
    public long IdCentro { get; set; }

    public long IdTipoCentro { get; set; }

    public string? TipoCentro { get; set; }

    public string? Nombre { get; set; }

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public string? Email { get; set; }

    public string? Web { get; set; }

    public long? IdCentroDato { get; set; }

    public long? IdCentroCentroDato { get; set; }

    public string? Dato { get; set; }

    public string? Texto { get; set; }

    public long? IdCentroRedSocial { get; set; }

    public long? IdCentroCentroRedSocial { get; set; }

    public long? IdRedSocial { get; set; }

    public string? RedSocial { get; set; }

    public string? NombreRedSocial { get; set; }
}
