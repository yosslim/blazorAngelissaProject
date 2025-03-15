using System;
using System.Collections.Generic;

namespace angelissaPastryShop.Data;

public partial class Presentacion
{
    public int PresentacionID { get; set; }

    public string Nombre { get; set; }

    public virtual ICollection<DetallesOrden> DetallesOrdens { get; set; } = new List<DetallesOrden>();

    public virtual ICollection<PreciosProducto> PreciosProductos { get; set; } = new List<PreciosProducto>();
}
