using AutoMapper;
using SistemaVentas.BLL.Servicios.Contratos;
using SistemaVentas.DAL.Repositorios.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.Model;
using Microsoft.EntityFrameworkCore;

namespace SistemaVentas.BLL.Servicios
{
    public class ProductoService : IProductoService
    {
        private readonly IGenericRepository<Producto> _productoRepositorio;
        private readonly IMapper _mapper;

        public ProductoService(IGenericRepository<Producto> productoRepositorio, IMapper mapper)
        {
            _productoRepositorio = productoRepositorio;
            _mapper = mapper;
        }

        public async Task<List<ProductoDTO>> Lista()
        {
            try
            {
                var queryProducto = await _productoRepositorio.Consulta();

                var listaProductos = queryProducto.Include(categoria => categoria.IdCategoriaNavigation).ToList();

                return _mapper.Map<List<ProductoDTO>>(listaProductos.ToList());


            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ProductoDTO> Crear(ProductoDTO modelo)
        {
            try
            {
                var productoCreado = await _productoRepositorio.Crear(_mapper.Map<Producto>(modelo));

                if (productoCreado.IdProducto == 0)
                {
                    throw new TaskCanceledException("No se pudo Crear");
                }

                return _mapper.Map<ProductoDTO>(productoCreado);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Editar(ProductoDTO modelo)
        {
            try
            {
                var productoModelo = _mapper.Map<Producto>(modelo);

                var productoEncontrado = await _productoRepositorio.Obtener(filtro => filtro.IdProducto == productoModelo.IdProducto);

                if (productoEncontrado == null)
                {
                    throw new TaskCanceledException("El producto no existe");
                }

                productoEncontrado.Nombre = productoModelo.Nombre;
                productoEncontrado.IdCategoria = productoModelo.IdCategoria;
                productoEncontrado.Stock = productoModelo.Stock;
                productoEncontrado.Precio = productoModelo.Precio;
                productoEncontrado.EsActivo = productoModelo.EsActivo;


                bool respuesta = await _productoRepositorio.Editar(productoEncontrado);

                if (!respuesta)
                {
                    throw new TaskCanceledException("No se pudo Editar el producto");
                }

                return respuesta;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            try
            {
                var productoEncontrado = await _productoRepositorio.Obtener(filtro => filtro.IdProducto == id);

                if (productoEncontrado == null)
                {
                    throw new TaskCanceledException("El producto no existe");
                }

                bool respuesta = await _productoRepositorio.Delete(productoEncontrado);

                if (!respuesta)
                {
                    throw new TaskCanceledException("El producto no se pudo eliminar");
                }

                return respuesta;

            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
