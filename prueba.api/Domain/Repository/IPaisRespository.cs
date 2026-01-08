using Prueba.Domain;
namespace Domain.repository;
public interface IPaisRepository
{
    Task<Pais?> ObtenerPorIdAsync(Guid id);
    Task<Pais?> ObtenerPorNombreAsync(string nombre);
    Task<List<Pais>> ObtenerTodosAsync();

    Task CrearAsync(Pais pais);
    Task ActualizarAsync(Pais pais);
    Task EliminarAsync(Pais pais);
}