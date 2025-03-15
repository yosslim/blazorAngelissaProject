using angelissaPastryShop.Data;

namespace angelissaPastryShop.Repository
{
    public interface IProductoRepositorio
    {
        public Task<List<ProductoDTO>> GetProductos();
        public Task<Producto> GetProductoId(int idOrder);
        public Task<List<Categoria>> ObtenerCategorias();
        public Task<List<Producto>> ObtenerPorCategoria(int categoriaId);
        public Task<List<Producto>> BuscarPorNombre(string busqueda);
    }
}
