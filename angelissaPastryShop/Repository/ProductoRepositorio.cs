using angelissaPastryShop.Models;
using Microsoft.EntityFrameworkCore;

namespace angelissaPastryShop.Repository
{
    public class ProductoRepositorio : IProductoRepositorio
    {
        private readonly AngelissaShopDbv0Context _context;
        public ProductoRepositorio(AngelissaShopDbv0Context context)
        {
            this._context = context;
        }

        public Task<Producto> GetProductoId(int idOrder)
        {
            throw new NotImplementedException();
        }

        //public async Task<List<Producto>> GetProductos()
        //{
        //    return await _context.Productos.ToListAsync();
        //}

        public async Task<List<ProductoDTO>> GetProductos()
        {
            var productos = await _context.Productos
                .Include(p => p.Categoria)
                .Select(p => new ProductoDTO
                {
                    ProductoID = p.ProductoId,
                    Nombre = p.Nombre,
                    Imagen = p.Imagen,
                    Categoria = p.Categoria.Nombre,
                    CategoriaID = p.CategoriaId,
                    Presentaciones = _context.PreciosProductos
                        .Where(pp => pp.ProductoId == p.ProductoId)
                        .Select(pp => new PresentacionDTO
                        {
                            Presentacion = pp.Presentacion.Nombre,
                            Precio = pp.Precio,
                            PresentacionID = pp.PresentacionId
                        }).ToList()
                }).ToListAsync();

            return productos;
        }

        public async Task<List<Categoria>> ObtenerCategorias()
        {
            return await _context.Categorias.ToListAsync();
        }
        public async Task<List<Producto>> ObtenerPorCategoria(int categoriaId)
        {
            return await _context.Productos
                                 .Where(p => p.CategoriaId == categoriaId)
                                 .ToListAsync();
        }
        public async Task<List<Producto>> BuscarPorNombre(string busqueda)
        {
            return await _context.Productos
                                 .Where(p => p.Nombre.Contains(busqueda))
                                 .ToListAsync();
        }
    }
}
