using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class TblPersona
{
    public long IdPersona { get; set; }

    public string? Apellidos { get; set; }

    public string? Nombres { get; set; }

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public virtual ICollection<TblPersonaDato> TblPersonaDatos { get; set; } = new List<TblPersonaDato>();

    public virtual ICollection<TblPersonaProfesion> TblPersonaProfesions { get; set; } = new List<TblPersonaProfesion>();

    public virtual ICollection<TblPersonaRedSocial> TblPersonaRedSocials { get; set; } = new List<TblPersonaRedSocial>();
}
