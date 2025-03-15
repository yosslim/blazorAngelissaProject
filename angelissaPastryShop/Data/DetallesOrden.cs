using System;
using System.Collections.Generic;

namespace angelissaPastryShop.Data;

public partial class DetallesOrden
{
    public int DetalleID { get; set; }

    public int OrdenID { get; set; }

    public int ProductoID { get; set; }

    public int PresentacionID { get; set; }

    public int Cantidad { get; set; }

    public decimal PrecioUnitario { get; set; }

    public decimal? Subtotal { get; set; }

    public virtual Orden Orden { get; set; }

    public virtual Presentacion Presentacion { get; set; }

    public virtual Producto Producto { get; set; }
}
