using AutoMapper;
using SistemaVentas.BLL.Servicios.Contratos;
using SistemaVentas.DAL.Repositorios.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.Model;
using Microsoft.EntityFrameworkCore;

namespace SistemaVentas.BLL.Servicios
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IGenericRepository<Usuario> _usuarioRepositorio;
        private readonly IMapper _mapper;

        public UsuarioService(IGenericRepository<Usuario> usuarioRepositorio, IMapper mapper)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _mapper = mapper;
        }

        public async Task<List<UsuarioDTO>> Lista()
        {
            try
            {
                var queryUsuario = await _usuarioRepositorio.Consulta();
                var listaUsuarios = queryUsuario.Include(rol => rol.IdRolNavigation).ToList();

                return _mapper.Map<List<UsuarioDTO>>(listaUsuarios);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<SesionDTO> ValidarCredenciales(string correo, string clave)
        {
            try
            {
                var queryUsuario = await _usuarioRepositorio.Consulta(filtro => filtro.Correo == correo && filtro.Clave == clave);
                if (queryUsuario.FirstOrDefault() == null)
                {
                    throw new TaskCanceledException("Usuario no existe");
                }

                Usuario devolverUsuario = queryUsuario.Include(rol => rol.IdRolNavigation).First();

                return _mapper.Map<SesionDTO>(devolverUsuario);

            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task<UsuarioDTO> Crear(UsuarioDTO modelo)
        {
            try
            {
                var usuarioCreado = await _usuarioRepositorio.Crear(_mapper.Map<Usuario>(modelo));

                if (usuarioCreado.IdUsuario == 0)
                {
                    throw new TaskCanceledException("Error al crear usuario");
                }

                var query = await _usuarioRepositorio.Consulta(filtro => filtro.IdUsuario == usuarioCreado.IdUsuario);

                usuarioCreado = query.Include(rol => rol.IdRolNavigation).First();

                return _mapper.Map<UsuarioDTO>(usuarioCreado);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Editar(UsuarioDTO modelo)
        {
            try
            {
                var usuarioModelo = _mapper.Map<Usuario>(modelo);

                var usuarioEncontrado = await _usuarioRepositorio.Obtener(filtro => filtro.IdUsuario == usuarioModelo.IdUsuario);


                if (usuarioEncontrado == null)
                {
                    throw new TaskCanceledException("Usuario No Existe");
                }

                usuarioEncontrado.NombreCompleto = usuarioModelo.NombreCompleto;
                usuarioEncontrado.Correo = usuarioModelo.Correo;
                usuarioEncontrado.IdRol = usuarioModelo.IdRol;
                usuarioEncontrado.Clave = usuarioModelo.Clave;
                usuarioEncontrado.EsActivo = usuarioModelo.EsActivo;

                bool respuesta = await _usuarioRepositorio.Editar(usuarioEncontrado);

                if (!respuesta)
                {
                    throw new TaskCanceledException("No se pudo Editar");
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
                var usuarioEncontrado = await _usuarioRepositorio.Obtener(filtro => filtro.IdUsuario == id);

                if (usuarioEncontrado == null)
                {
                    throw new TaskCanceledException("Usuario No Existe");
                }

                bool respuesta = await _usuarioRepositorio.Delete(usuarioEncontrado);


                if (!respuesta)
                {
                    throw new TaskCanceledException("Usuario No se pudo eliminar");
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
