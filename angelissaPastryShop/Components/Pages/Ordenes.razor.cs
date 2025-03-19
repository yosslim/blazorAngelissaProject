using angelissaPastryShop.Models;
using angelissaPastryShop.Repository;
using angelissaPastryShop.Services;
using Microsoft.AspNetCore.Components;

namespace angelissaPastryShop.Components.Pages
{
    public partial class Ordenes
    {
        [Inject] IOrdenRepositorio OrdenRepositorio { get; set; }

        private List<Orden> OrdenesList = new();
        private List<Orden> OrdenesPaginadas = new();
        private int PaginaActual = 1;
        private int ElementosPorPagina = 5;
        private bool MostrarConfirmacion = false;      
        private string MensajeConfirmacion = "";
        private Func<Task> AccionConfirmada;
        private Orden? OrdenSeleccionada;

        private int TotalOrdenesAct { get; set; }
       
        protected override async Task OnInitializedAsync()
        {
            OrdenesList = await OrdenRepositorio.ObtenerOrdenesActivas();
            
            TotalOrdenesAct = await OrdenRepositorio.TotalOrdenesActivas();
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

        private void CerrarConfirmacion()
        {
            MostrarConfirmacion = false;
        }

        private void ConfirmarCompletarOrden(int ordenId)
        {
            MensajeConfirmacion = "¿Seguro que deseas completar esta orden?";
            AccionConfirmada = async () => await CompletarOrden(ordenId);
            MostrarConfirmacion = true;
        }

        private void ConfirmarCancelarOrden(int ordenId)
        {
            MensajeConfirmacion = "¿Seguro que deseas cancelar esta orden?";
            AccionConfirmada = async () => await CancelarOrden(ordenId);
            MostrarConfirmacion = true;
        }

        private async Task EjecutarAccionConfirmada()
        {
            if (AccionConfirmada != null)
            {
                await AccionConfirmada.Invoke();
            }
            MostrarConfirmacion = false;
            StateHasChanged();
        }    

        private async Task CompletarOrden(int ordenId)
        {
            await OrdenRepositorio.CompletarOrden(ordenId);
            OrdenesList = await OrdenRepositorio.ObtenerOrdenesActivas();
            TotalOrdenesAct = await OrdenRepositorio.TotalOrdenesActivas();
            ActualizarPaginacion();
        }
      
        private async Task CancelarOrden(int ordenId)
        {
            await OrdenRepositorio.EliminarOrden(ordenId);
            OrdenesList = await OrdenRepositorio.ObtenerOrdenesActivas();
            TotalOrdenesAct = await OrdenRepositorio.TotalOrdenesActivas();
            ActualizarPaginacion();
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
            OrdenesPaginadas = OrdenesList
                .Skip((PaginaActual - 1) * ElementosPorPagina)
                .Take(ElementosPorPagina)
                .ToList();
        }

        private int TotalPaginas => (int)Math.Ceiling((double)OrdenesList.Count / ElementosPorPagina);
        private bool PuedeRetroceder => PaginaActual > 1;
        private bool PuedeAvanzar => PaginaActual < TotalPaginas;
    }
}
