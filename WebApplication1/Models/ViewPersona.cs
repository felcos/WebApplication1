using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class ViewPersona
{
    public long? IdPersonaProfesion { get; set; }
    public long IdPersona { get; set; }

    public string? Apellidos { get; set; }

    public string? Nombres { get; set; }

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

   

    public long? IdProfesion { get; set; }

    public string? Profesion { get; set; }

    public long? IdPersonaRedSocial { get; set; }

    public long? IdRedSocial { get; set; }

    public string? NombreRedSocial { get; set; }

    public string? RedSocial { get; set; }
}
