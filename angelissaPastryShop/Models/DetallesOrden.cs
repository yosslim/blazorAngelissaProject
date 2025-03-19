using System;
using System.Collections.Generic;

namespace angelissaPastryShop.Models;

public partial class DetallesOrden
{
    public int DetalleId { get; set; }

    public int OrdenId { get; set; }

    public int ProductoId { get; set; }

    public int PresentacionId { get; set; }

    public int Cantidad { get; set; }

    public decimal PrecioUnitario { get; set; }

    public decimal? Subtotal { get; set; }

    public virtual Orden Orden { get; set; }

    public virtual Presentacione Presentacion { get; set; }

    public virtual Producto Producto { get; set; }
}
