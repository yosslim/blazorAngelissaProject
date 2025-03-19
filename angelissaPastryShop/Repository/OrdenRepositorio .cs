using angelissaPastryShop.Components.Pages;
using angelissaPastryShop.Models;
using Microsoft.EntityFrameworkCore;

namespace angelissaPastryShop.Repository
{
    public class OrdenRepositorio : IOrdenRepositorio
    {
        private readonly AngelissaShopDbv0Context _context;

        public OrdenRepositorio(AngelissaShopDbv0Context context)
        {
            _context = context;
        }

        public async Task GuardarOrden(Orden orden)
        {
            _context.Ordenes.Add(orden);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Orden>> ObtenerHistorialOrdenes()
        {
            return await _context.Ordenes.Where(o => o.Estado == "Completada").Include(o => o.DetallesOrdens).ThenInclude(d => d.Producto).ToListAsync();
        }

        public async Task<Orden> ObtenerOrdenPorId(int id)
        {
            //return await _context.Ordenes.Include(o => o.DetallesOrdens).ThenInclude(d => d.Producto)
            //                             .FirstOrDefaultAsync(o => o.OrdenID == id);

            return await _context.Ordenes.Include(o => o.DetallesOrdens).ThenInclude(d => d.Producto)
                                                                        .ThenInclude(p => p.PreciosProductos) // Incluir la relación con PrecioProducto
                                                                        .ThenInclude(pp => pp.Presentacion) // Incluir la relación con Presentacion
                                                                        .FirstOrDefaultAsync(o => o.OrdenId == id);
        }

        public async Task<List<Orden>> ObtenerOrdenesActivas() 
        {
            return await _context.Ordenes.Where(o => o.Estado == "Pendiente").OrderByDescending(o => o.Fecha).ToListAsync();
        }

        public async Task<int> TotalOrdenesActivas()
        {
            return await _context.Ordenes.Where(o => o.Estado == "Pendiente").CountAsync();
        }

        public async Task<int> TotalOrdenesCompletas()
        {
            return await _context.Ordenes.Where(o => o.Estado == "Completada").CountAsync();
        }

        public async Task EliminarOrden(int ordenId) 
        {
            var orden = await _context.Ordenes.FindAsync(ordenId);
            if (orden != null)
            {
                // Eliminar registros dependientes en la tabla DetallesOrden
                var detalles = _context.DetallesOrdens
                                      .Where(d => d.OrdenId == ordenId)
                                      .ToList();
                _context.DetallesOrdens.RemoveRange(detalles);

                // Eliminar el registro principal
                _context.Ordenes.Remove(orden);

                // Guardar cambios              
                await _context.SaveChangesAsync();
            }
        }

        public async Task CompletarOrden(int ordenId)
        {
            var orden = await _context.Ordenes.FindAsync(ordenId);
            if (orden != null)
            {
                orden.Estado = "Completada";
                _context.Ordenes.Update(orden);
                await _context.SaveChangesAsync();
            }
        }
    }
}
