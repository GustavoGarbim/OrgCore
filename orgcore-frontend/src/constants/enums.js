// Enums do sistema OrgCore

export const Cargos = {
    DESENVOLVEDOR: 1,
    ANALISTA: 2,
    GERENTE: 3,
    DIRETOR: 4,
    EXECUTIVO: 5
}

export const CargosLabels = {
    [Cargos.DESENVOLVEDOR]: 'Desenvolvedor',
    [Cargos.ANALISTA]: 'Analista',
    [Cargos.GERENTE]: 'Gerente',
    [Cargos.DIRETOR]: 'Diretor',
    [Cargos.EXECUTIVO]: 'Executivo'
}

export const TipoQuestao = {
    TEXTO_LIVRE: 1,
    NOTA: 2,
    MULTIPLA_ESCOLHA: 3
}

export const TipoQuestaoLabels = {
    [TipoQuestao.TEXTO_LIVRE]: 'Texto Livre',
    [TipoQuestao.NOTA]: 'Nota',
    [TipoQuestao.MULTIPLA_ESCOLHA]: 'Múltipla Escolha'
}

// Helper para verificar se cargo tem permissões de gestor
export const isGestor = (cargo) => {
    return cargo >= Cargos.GERENTE
}
