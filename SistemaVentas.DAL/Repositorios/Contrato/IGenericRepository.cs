using System.Linq.Expressions;


namespace SistemaVentas.DAL.Repositorios.Contrato
{
    public interface IGenericRepository<TModel> where TModel : class
    {
        Task<TModel> Obtener(Expression<Func<TModel, bool>> filtro);
        Task<TModel> Crear(TModel modelo);
        Task<bool> Editar(TModel modelo);
        Task<bool> Delete(TModel modelo);
        Task<IQueryable<TModel>> Consulta(Expression<Func<TModel, bool>> filtro = null);

    }
}
