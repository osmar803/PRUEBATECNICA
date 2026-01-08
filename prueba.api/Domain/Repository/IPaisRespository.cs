using Prueba.Domain;
public interface IPaisRepository
{
    Task<Pais?> ObtenerPorIdAsync(Guid id);
    Task<Pais?> ObtenerPorNombreAsync(string nombre);
    Task<List<Pais>> ObtenerTodosAsync();

    Task AgregarAsync(Pais pais);
    Task ActualizarAsync(Pais pais);
    Task EliminarAsync(Guid id);
}
