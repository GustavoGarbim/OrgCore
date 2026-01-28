import api from './api'

export const formularioService = {
    async criarFormulario(data) {
        const response = await api.post('/Formularios', data)
        return response.data
    },

    async listarFormulariosPorEmpresa(empresaId) {
        const response = await api.get('/Formularios', { params: { empresaId } })
        return response.data
    },

    async responderFormulario(data) {
        const response = await api.post('/Formularios/responder', data)
        return response.data
    },

    async obterRelatorio(id) {
        const response = await api.get(`/Formularios/${id}`)
        return response.data
    }
}
