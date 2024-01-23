using SistemaVenta.DTO;


namespace SistemaVentas.BLL.Servicios.Contratos
{
    public interface IRolService
    {
        Task<List<RolDTO>> Lista();

    }
}
