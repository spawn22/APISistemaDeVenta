using AutoMapper;
using SistemaVentas.BLL.Servicios.Contratos;
using SistemaVentas.DAL.Repositorios.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.Model;

namespace SistemaVentas.BLL.Servicios
{
    public class CategoriaService : ICategoriaService
    {
        private readonly IGenericRepository<Categoria> _categoriaRepositorio;
        private readonly IMapper _mapper;

        public CategoriaService(IGenericRepository<Categoria> categoriaRepositorio, IMapper mapper)
        {
            _categoriaRepositorio = categoriaRepositorio;
            _mapper = mapper;
        }

        public async Task<List<CategoriaDTO>> Lista()
        {
            try
            {
                var listaCategorias = await _categoriaRepositorio.Consulta();
                return _mapper.Map<List<CategoriaDTO>>(listaCategorias.ToList());
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
