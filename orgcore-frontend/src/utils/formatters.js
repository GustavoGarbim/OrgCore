// Formatadores de dados

export const formatarCPF = (cpf) => {
    if (!cpf) return ''
    cpf = cpf.replace(/[^\d]/g, '')
    return cpf.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, '$1.$2.$3-$4')
}

export const formatarCNPJ = (cnpj) => {
    if (!cnpj) return ''
    cnpj = cnpj.replace(/[^\d]/g, '')
    return cnpj.replace(/(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})/, '$1.$2.$3/$4-$5')
}

export const formatarData = (data) => {
    if (!data) return ''
    const date = new Date(data)
    return date.toLocaleDateString('pt-BR')
}

export const formatarDataHora = (data) => {
    if (!data) return ''
    const date = new Date(data)
    return date.toLocaleString('pt-BR')
}

export const formatarNota = (nota) => {
    if (nota === null || nota === undefined) return '-'
    return Number(nota).toFixed(1)
}
