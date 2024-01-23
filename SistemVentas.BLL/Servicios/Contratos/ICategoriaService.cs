using SistemaVenta.DTO;



namespace SistemaVentas.BLL.Servicios.Contratos
{
    public interface ICategoriaService
    {
        Task<List<CategoriaDTO>> Lista();
    }
}
