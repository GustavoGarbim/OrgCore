export interface Empresa {
    id?: string;
    nomeFantasia: string;
    cnpj: string;
    ativo?: boolean;
}

export interface EmpresaDTO {
    nomeFantasia: string;
    cnpj: string;
    nomeAdmin: string;
    emailAdmin: string;
}

export enum EnumCargo {
    Desenvolvedor = 1,
    Analista = 2,
    Gerente = 3,
    Diretor = 4,
    Executivo = 5
}

export interface AtribuirCargoDTO {
    colaboradorId: string;
    cargo: EnumCargo;
}
