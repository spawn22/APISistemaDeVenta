using SistemaVenta.DTO;

namespace SistemaVentas.BLL.Servicios.Contratos
{
    public interface IDashboardService
    {
        Task<DashboardDTO> Resumen();
    }
}
