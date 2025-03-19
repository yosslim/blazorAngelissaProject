using angelissaPastryShop.Components.Pages;
using angelissaPastryShop.Models;

namespace angelissaPastryShop.Repository
{
    public interface IOrdenRepositorio
    {
        Task GuardarOrden(Orden orden);
        Task<List<Orden>> ObtenerHistorialOrdenes();
        Task<Orden?> ObtenerOrdenPorId(int id);
        Task<List<Orden>> ObtenerOrdenesActivas();
        Task EliminarOrden(int ordenId);
        Task<int> TotalOrdenesActivas();
        Task<int> TotalOrdenesCompletas();
        Task CompletarOrden(int ordenId);
    }
}
