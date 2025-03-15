using angelissaPastryShop.Data;
using angelissaPastryShop.Repository;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace angelissaPastryShop.Services
{
    public class CarritoServicio
    { 

        private readonly IOrdenRepositorio _context; // Inyectar repositorio de base de datos
       
        public CarritoServicio(IOrdenRepositorio context)
        {
            _context = context;
        }

        public List<ProductoCarrito> productosEnCarrito = new List<ProductoCarrito>();      
        

        public void AgregarProducto(ProductoDTO producto, PresentacionDTO presentacion)
        {
            // Buscar por combinación de ProductoID + PresentacionID
            var productoEnCarrito = productosEnCarrito
                .FirstOrDefault(p => p.Id == producto.ProductoID && p.PresentacionID == presentacion.PresentacionID);

            if (productoEnCarrito != null)
            {
                productoEnCarrito.Cantidad++; // Si ya existe esa combinación, aumentar cantidad
            }
            else
            {
                productosEnCarrito.Add(new ProductoCarrito
                {
                    Id = producto.ProductoID,
                    Nombre = producto.Nombre,
                    Precio = presentacion.Precio,
                    Presentacion = presentacion.Presentacion,
                    PresentacionID = presentacion.PresentacionID,
                    Cantidad = 1
                });
            }
        }      

        public void VaciarCarrito()
        {
            productosEnCarrito.Clear();
        }

        public void EliminarProducto(int productoId)
        {
            var item = productosEnCarrito.FirstOrDefault(i => i.Id == productoId);
            if (item != null)
            {
                productosEnCarrito.Remove(item);
            }
        }
     
        public async Task ConfirmarOrden()
        {
            if (!productosEnCarrito.Any())
                return;

            var nuevaOrden = new Orden
            {               
                Fecha = DateTime.Now,
                Estado = "Pendiente",
                Total = productosEnCarrito.Sum(p => p.Precio * p.Cantidad),   
              
                DetallesOrdens = productosEnCarrito.Select(p => new DetallesOrden
                {
                    //OrdenID:  EF Core maneja la relación y el OrderId correspondiente se agrega automáticamente, sin necesidad de hacer dos inserts separados, EF Core maneja automáticamente las claves foráneas al usar SaveChanges()
                    ProductoID = p.Id,
                    PresentacionID = p.PresentacionID,
                    Cantidad = p.Cantidad,
                    PrecioUnitario = p.Precio
                }).ToList()
            };

            await _context.GuardarOrden(nuevaOrden);          

            productosEnCarrito.Clear();
        }


        public decimal ObtenerTotal()
        {           
            return productosEnCarrito.Sum(i => i.Precio * i.Cantidad);
        }
    }

    public class ProductoCarrito
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public decimal Precio { get; set; }
        public string Presentacion { get; set; }
        public int PresentacionID { get; set; }
        public int Cantidad { get; set; }
    }
}
