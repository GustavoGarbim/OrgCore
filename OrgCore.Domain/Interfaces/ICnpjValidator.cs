namespace OrgCore.Domain.Interfaces
{
    public interface ICnpjValidator
    {
        Task<bool> ValidarEmpresa(string cnpj);
    }
}
