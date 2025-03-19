using System;
using System.Collections.Generic;

namespace angelissaPastryShop.Models;

public partial class PreciosProducto
{
    public int PrecioId { get; set; }

    public int ProductoId { get; set; }

    public int PresentacionId { get; set; }

    public decimal Precio { get; set; }

    public virtual Presentacione Presentacion { get; set; }

    public virtual Producto Producto { get; set; }
}
