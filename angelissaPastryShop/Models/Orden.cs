using System;
using System.Collections.Generic;

namespace angelissaPastryShop.Models;

public partial class Orden
{
    public int OrdenId { get; set; }

    public DateTime Fecha { get; set; }

    public string Estado { get; set; }

    public decimal Total { get; set; }

    public int? UsuarioId { get; set; }

    public int? SucursalId { get; set; }

    public int? ClienteId { get; set; }

    public virtual Cliente Cliente { get; set; }

    public virtual ICollection<DetallesOrden> DetallesOrdens { get; set; } = new List<DetallesOrden>();

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();

    public virtual Sucursale Sucursal { get; set; }

    public virtual Usuario Usuario { get; set; }
}
