namespace OrgCore.Domain.Interfaces
{
    public interface IDocValidator
    {
        Task<bool> ValidarEmpresa(string cnpj);
        Task<bool> ValidarPessoa(string cpf);
    }
}
