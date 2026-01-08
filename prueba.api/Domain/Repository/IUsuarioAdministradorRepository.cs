 using Prueba.Domain;
public interface IUsuarioAdministradorRepository
{
    Task<UsuarioAdministrador?> ObtenerPorUsuarioAsync(string usuario);
    Task AgregarAsync(UsuarioAdministrador usuarioAdministrador);
}
