using Prueba.Domain.Exceptions;
namespace Prueba.Domain;
public class Departamento
{
    public Guid Id { get; private set; }
    public string Nombre { get; private set; }
    public Guid PaisId { get; private set; }

    private Departamento() { }

    public Departamento(string nombre, Guid paisId)
    {
        CambiarNombre(nombre);
        CambiarPais(paisId);
        Id = Guid.NewGuid();
    }

   public void CambiarNombre(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ReglaNegocioException("El nombre del departamento es obligatorio");

        Nombre = nombre.Trim();
    }

    private void CambiarPais(Guid paisId)
    {
        if (paisId == Guid.Empty)
            throw new ReglaNegocioException("El país es obligatorio");

        PaisId = paisId;
    }
}
