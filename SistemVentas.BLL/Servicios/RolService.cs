using AutoMapper;
using SistemaVentas.BLL.Servicios.Contratos;
using SistemaVentas.DAL.Repositorios.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.Model;

namespace SistemaVentas.BLL.Servicios
{
    public class RolService : IRolService
    {

        private readonly IGenericRepository<Rol> _rolRepositorio;
        private readonly IMapper _mapper;

        public RolService(IGenericRepository<Rol> rolRepositorio, IMapper mapper)
        {
            _rolRepositorio = rolRepositorio;
            _mapper = mapper;
        }

        public async Task<List<RolDTO>> Lista()
        {
            try
            {
                var listaRoles = await _rolRepositorio.Consulta();
                return _mapper.Map<List<RolDTO>>(listaRoles.ToList()); 
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
