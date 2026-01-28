import api from './api'

export const authService = {
    async gerarToken(cpf) {
        const response = await api.post('/Auth/gerar-token', { cpf })
        return response.data
    }
}
