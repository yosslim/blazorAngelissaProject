using angelissaPastryShop.Models;
using angelissaPastryShop.Repository;
using angelissaPastryShop.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace angelissaPastryShop.Components.Pages
{
    public partial class Pedidos
    {
        [Inject] private IProductoRepositorio ProductoRepositorio { get; set; } /* Permite que Blazor asigne el servicio automáticamente.
                                                                                   * La inyección con [Inject] es más
                                                                                   recomendada porque Blazor maneja la creación de instancias.*/

        [Inject] private CarritoServicio CarritoServicio { get; set; }
        [Inject] private IJSRuntime JS { get; set; }
        [Inject] private IOrdenRepositorio OrdenRepositorio { get; set; }
        private List<ProductoDTO> ProductosPaginadas = new List<ProductoDTO>();
        private int PaginaActual = 1;
        private int ElementosPorPagina = 5;
        private int SelectedCategoryId { get; set; }
        private int? SelectedCategory { get; set; } = null;
        private string SearchTerm = "";

      
       
        private List<Categoria> Categorias = new();       

        //private List<Producto> productos = new();
        private List<ProductoDTO> productos = new();

        /*
         * ProductosFiltrados es una propiedad calculada. Esto significa que su valor se actualiza automáticamente cada vez que cambian Products, SearchTerm o SelectedCategory.
         * Por eso, no necesita estar en OnInitializedAsync, ya que se recalcula dinámicamente cuando los datos cambian
         */

        private List<ProductoDTO> ProductosFiltrados => productos.Where(p => (string.IsNullOrEmpty(SearchTerm) || p.Nombre.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase)) &&
                                                     (!SelectedCategory.HasValue || p.CategoriaID == SelectedCategory.GetValueOrDefault())).ToList();
        protected override async Task OnInitializedAsync()
        {
            var res = string.Empty;
            try
            {
                productos = await ProductoRepositorio.GetProductos();
                Categorias = await ProductoRepositorio.ObtenerCategorias();
            }
            catch (Exception e)
            {

                res = e.Message;
            }
        }
        private async Task ConfirmarOrden()
        {
            await CarritoServicio.ConfirmarOrden();
        }

        private void AgregarAlCarrito(ProductoDTO producto, PresentacionDTO presentacion)
        {
            CarritoServicio.AgregarProducto(producto, presentacion);
            // Desplazar la vista hacia el carrito
            JS.InvokeVoidAsync("scrollToCart");
            StateHasChanged(); // Forzar actualización de la UI
        }

        private void EliminarDelCarrito(int productoId)
        {
            CarritoServicio.EliminarProducto(productoId);
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
            ProductosPaginadas = ProductosFiltrados
                .Skip((PaginaActual - 1) * ElementosPorPagina)
                .Take(ElementosPorPagina)
                .ToList();
        }

        private int TotalPaginas => (int)Math.Ceiling((double)ProductosFiltrados.Count / ElementosPorPagina);
        private bool PuedeRetroceder => PaginaActual > 1;
        private bool PuedeAvanzar => PaginaActual < TotalPaginas;
    }
}
