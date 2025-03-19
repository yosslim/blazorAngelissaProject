using System;
using System.Collections.Generic;

namespace angelissaPastryShop.Models;

public partial class Sucursal
{
    public int SucursalId { get; set; }

    public string Nombre { get; set; }

    public string Direccion { get; set; }

    public virtual ICollection<Orden> Ordenes { get; set; } = new List<Orden>();
}
