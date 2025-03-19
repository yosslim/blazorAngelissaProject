using System;
using System.Collections.Generic;

namespace angelissaPastryShop.Models;

public partial class Producto
{
    public int ProductoId { get; set; }

    public string Nombre { get; set; }

    public string Imagen { get; set; }

    public int CategoriaId { get; set; }

    public virtual Categoria Categoria { get; set; }

    public virtual ICollection<DetallesOrden> DetallesOrdens { get; set; } = new List<DetallesOrden>();

    public virtual ICollection<PreciosProducto> PreciosProductos { get; set; } = new List<PreciosProducto>();
}
