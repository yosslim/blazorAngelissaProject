using System;
using System.Collections.Generic;

namespace angelissaPastryShop.Models;

public partial class Cliente
{
    public int ClienteId { get; set; }

    public string Nombre { get; set; }

    public string Correo { get; set; }

    public string Telefono { get; set; }

    public string Direccion { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<Orden> Ordenes { get; set; } = new List<Orden>();
}
