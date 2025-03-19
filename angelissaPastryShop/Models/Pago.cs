using System;
using System.Collections.Generic;

namespace angelissaPastryShop.Models;

public partial class Pago
{
    public int PagoId { get; set; }

    public int OrdenId { get; set; }

    public string MetodoPago { get; set; }

    public decimal Monto { get; set; }

    public DateTime? FechaPago { get; set; }

    public virtual Orden Orden { get; set; }
}
