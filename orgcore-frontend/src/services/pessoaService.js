import api from './api'

export const pessoaService = {
    async registrarPessoa(data) {
        const response = await api.post('/Pessoa', data)
        return response.data
    },

    async buscarPessoa(id) {
        const response = await api.get('/Pessoa', { params: { id } })
        return response.data
    }
}
