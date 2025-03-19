namespace angelissaPastryShop.Models
{
    public class ProductoDTO
    {
        public int ProductoID { get; set; }

        public string Nombre { get; set; }

        public string Imagen { get; set; }

        public string Categoria { get; set; }
        public int CategoriaID { get; set; }

        public List<PresentacionDTO> Presentaciones { get; set; }
    }
}
