import { defineStore } from 'pinia'
import { authService } from '../services/authService'
import { pessoaService } from '../services/pessoaService'

export const useAuthStore = defineStore('auth', {
    state: () => ({
        token: localStorage.getItem('orgcore_token') || null,
        user: JSON.parse(localStorage.getItem('orgcore_user') || 'null'),
        colaborador: JSON.parse(localStorage.getItem('orgcore_colaborador') || 'null'),
        isAuthenticated: !!localStorage.getItem('orgcore_token')
    }),

    getters: {
        getCargo: (state) => state.colaborador?.cargo || null,
        isGestor: (state) => {
            const cargo = state.colaborador?.cargo
            return cargo >= 3 // Gerente ou superior
        },
        getUserName: (state) => state.user?.Nome || 'Usuário'
    },

    actions: {
        async login(cpf) {
            try {
                const response = await authService.gerarToken(cpf)

                // Buscar dados da pessoa pelo CPF
                // Nota: precisamos ajustar isso pois o endpoint busca por ID, não CPF
                // Por enquanto, vamos apenas salvar o token

                this.token = response.token || 'mock_token' // API retorna apenas mensagem
                this.isAuthenticated = true

                localStorage.setItem('orgcore_token', this.token)

                // TODO: Buscar dados do usuário logado
                const mockUser = { cpf, Nome: 'Usuário' }
                this.user = mockUser
                localStorage.setItem('orgcore_user', JSON.stringify(mockUser))

                return { success: true }
            } catch (error) {
                console.error('Erro ao fazer login:', error)
                return { success: false, error: error.response?.data?.Mensagem || 'Erro ao fazer login' }
            }
        },

        logout() {
            this.token = null
            this.user = null
            this.colaborador = null
            this.isAuthenticated = false

            localStorage.removeItem('orgcore_token')
            localStorage.removeItem('orgcore_user')
            localStorage.removeItem('orgcore_colaborador')
        },

        setColaborador(colaborador) {
            this.colaborador = colaborador
            localStorage.setItem('orgcore_colaborador', JSON.stringify(colaborador))
        }
    }
})
