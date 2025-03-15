using System;
using System.Collections.Generic;

namespace angelissaPastryShop.Data;

public partial class Categoria
{
    public int CategoriaID { get; set; }

    public string Nombre { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
