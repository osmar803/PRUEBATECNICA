// opcional 2 
namespace Prueba.Domain;
public class UsuarioAdministrador
{
    public Guid Id { get; private set; }
    public string Usuario { get; private set; }
    public string ClaveHash { get; private set; }

    private UsuarioAdministrador() { }

    public UsuarioAdministrador(string usuario, string claveHash)
    {
        Id = Guid.NewGuid();
        CambiarUsuario(usuario);
        CambiarClave(claveHash);
    }

    private void CambiarUsuario(string usuario)
    {
        if (string.IsNullOrWhiteSpace(usuario))
            throw new ArgumentException("El usuario es obligatorio");

        Usuario = usuario.Trim();
    }

    private void CambiarClave(string hash)
    {
        if (string.IsNullOrWhiteSpace(hash))
            throw new ArgumentException("La clave es obligatoria");

        ClaveHash = hash;
    }
}
