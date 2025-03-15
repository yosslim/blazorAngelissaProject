using System;
using System.Collections.Generic;

namespace angelissaPastryShop.Data;

public partial class PreciosProducto
{
    public int PrecioID { get; set; }

    public int ProductoID { get; set; }

    public int PresentacionID { get; set; }

    public decimal Precio { get; set; }

    public virtual Presentacion Presentacion { get; set; }

    public virtual Producto Producto { get; set; }
}
