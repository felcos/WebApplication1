using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class TblPersonaProfesion
{
    public long Id { get; set; }

    public long? IdPersona { get; set; }

    public long? IdProfesion { get; set; }

    public virtual TblPersona? IdPersonaNavigation { get; set; }

    public virtual TblProfesion? IdProfesionNavigation { get; set; }
}
