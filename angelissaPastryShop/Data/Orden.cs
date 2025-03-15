using System;
using System.Collections.Generic;

namespace angelissaPastryShop.Data;

public partial class Orden
{
    public int OrdenID { get; set; }

    public DateTime Fecha { get; set; }

    public string Estado { get; set; }

    public decimal Total { get; set; }

    public virtual ICollection<DetallesOrden> DetallesOrdens { get; set; } = new List<DetallesOrden>();
}
