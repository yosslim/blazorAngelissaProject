using System;
using System.Collections.Generic;

namespace angelissaPastryShop.Models;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public string NombreUsuario { get; set; }

    public string Pin { get; set; }

    public string Rol { get; set; }

    public virtual ICollection<Orden> Ordenes { get; set; } = new List<Orden>();
}
