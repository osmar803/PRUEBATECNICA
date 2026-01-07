// relacion muchos a muchos relacional 1 
namespace Prueba.Domain;
public class ColaboradorEmpresa
{
    public Guid ColaboradorId { get; private set; }
    public Guid EmpresaId { get; private set; }

    private ColaboradorEmpresa() { }

    public ColaboradorEmpresa(Guid colaboradorId, Guid empresaId)
    {
        if (colaboradorId == Guid.Empty || empresaId == Guid.Empty)
            throw new ArgumentException("Relación inválida");

        ColaboradorId = colaboradorId;
        EmpresaId = empresaId;
    }
}
