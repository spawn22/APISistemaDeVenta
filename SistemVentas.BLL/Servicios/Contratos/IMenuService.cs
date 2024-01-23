using SistemaVenta.DTO;

namespace SistemaVentas.BLL.Servicios.Contratos
{
    public interface IMenuService
    {
        Task<List<MenuDTO>> Lista(int idUsuario);
    }
}
