import api from './api'

export const colaboradorService = {
    async vincularColaborador(data) {
        const response = await api.post('/Colaborador', data)
        return response.data
    },

    async listarColaboradoresPorEmpresa(empresaId) {
        const response = await api.get(`/Colaborador/${empresaId}`)
        return response.data
    },

    async buscarColaborador(colaboradorId) {
        const response = await api.get(`/Colaborador/colaborador/${colaboradorId}`)
        return response.data
    }
}
