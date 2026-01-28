export interface Colaborador {
    colaboradorId: string;
    empresaId: string;
    nomeEmpresa: string;
    pessoaId: string;
    nomePessoa: string;
}

export interface CadastrarColaboradorDTO {
    empresaId: string;
    pessoaId: string;
}
