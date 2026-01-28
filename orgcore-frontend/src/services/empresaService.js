import api from './api'

export const empresaService = {
    async criarEmpresa(data) {
        const response = await api.post('/Empresa', data)
        return response.data
    },

    async listarEmpresas() {
        const response = await api.get('/Empresa')
        return response.data
    },

    async atribuirCargo(empresaId, data) {
        const response = await api.post(`/Empresa/${empresaId}`, data)
        return response.data
    }
}
