using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class TblProfesion
{
    public long IdProfesion { get; set; }

    public string? Profesion { get; set; }

    public virtual ICollection<TblPersonaProfesion> TblPersonaProfesions { get; set; } = new List<TblPersonaProfesion>();
}
