export interface Pessoa {
    id?: string;
    nome: string;
    cpf?: string;
    emailPessoal: string;
    usuarioId?: string;
}

export interface CriarPessoaDto {
    nome: string;
    cpf: string;
    emailPessoal: string;
}
