using System;
using System.Collections.Generic;

namespace angelissaPastryShop.Data;

public partial class Producto
{
    public int ProductoID { get; set; }

    public string Nombre { get; set; }

    public string Imagen { get; set; }

    public int CategoriaID { get; set; }

    public virtual Categoria Categoria { get; set; }

    public virtual ICollection<DetallesOrden> DetallesOrdens { get; set; } = new List<DetallesOrden>();

    public virtual ICollection<PreciosProducto> PreciosProductos { get; set; } = new List<PreciosProducto>();
}
