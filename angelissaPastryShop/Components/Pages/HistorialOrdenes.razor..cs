using angelissaPastryShop.Models;
using angelissaPastryShop.Repository;
using Microsoft.AspNetCore.Components;

namespace angelissaPastryShop.Components.Pages
{
    public partial class HistorialOrdenes
    {
        [Inject] private IOrdenRepositorio OrdenRepositorio { get; set; }

        private List<Orden> Ordenes = new();
        private Orden? OrdenSeleccionada;

        private List<Orden> OrdenesPaginadas = new();
        private int PaginaActual = 1;
        private int ElementosPorPagina = 5;
        private int TotalOrdenesComp { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Ordenes = await OrdenRepositorio.ObtenerHistorialOrdenes();
            TotalOrdenesComp = await OrdenRepositorio.TotalOrdenesCompletas();
            ActualizarPaginacion();
        }

        private async Task VerDetalles(int ordenId)
        {
            OrdenSeleccionada = await OrdenRepositorio.ObtenerOrdenPorId(ordenId);
        }

        private void CerrarModal()
        {
            OrdenSeleccionada = null;
        }

        private void PaginaAnterior()
        {
            if (PaginaActual > 1)
            {
                PaginaActual--;
                ActualizarPaginacion();
            }
        }

        private void PaginaSiguiente()
        {
            if (PaginaActual < TotalPaginas)
            {
                PaginaActual++;
                ActualizarPaginacion();
            }
        }

        private void ActualizarPaginacion()
        {
            OrdenesPaginadas = Ordenes
                .Skip((PaginaActual - 1) * ElementosPorPagina)
                .Take(ElementosPorPagina)
                .ToList();
        }

        private int TotalPaginas => (int)Math.Ceiling((double)Ordenes.Count / ElementosPorPagina);
        private bool PuedeRetroceder => PaginaActual > 1;
        private bool PuedeAvanzar => PaginaActual < TotalPaginas;
    }
}
