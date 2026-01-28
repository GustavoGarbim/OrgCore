import { defineStore } from 'pinia'
import { colaboradorService } from '../services/colaboradorService'

export const useColaboradorStore = defineStore('colaborador', {
    state: () => ({
        colaboradores: [],
        colaboradorAtual: null,
        loading: false,
        error: null
    }),

    getters: {
        getColaboradorById: (state) => (id) => {
            return state.colaboradores.find(col => col.colaboradorId === id)
        }
    },

    actions: {
        async listarColaboradoresPorEmpresa(empresaId) {
            this.loading = true
            this.error = null
            try {
                this.colaboradores = await colaboradorService.listarColaboradoresPorEmpresa(empresaId)
                return { success: true }
            } catch (error) {
                this.error = error.response?.data?.Mensagem || 'Erro ao listar colaboradores'
                console.error('Erro ao listar colaboradores:', error)
                return { success: false, error: this.error }
            } finally {
                this.loading = false
            }
        },

        async vincularColaborador(empresaId, pessoaId) {
            this.loading = true
            this.error = null
            try {
                const data = { EmpresaId: empresaId, PessoaId: pessoaId }
                const result = await colaboradorService.vincularColaborador(data)
                await this.listarColaboradoresPorEmpresa(empresaId) // Atualiza lista
                return { success: true, data: result }
            } catch (error) {
                this.error = error.response?.data?.Mensagem || 'Erro ao vincular colaborador'
                console.error('Erro ao vincular colaborador:', error)
                return { success: false, error: this.error }
            } finally {
                this.loading = false
            }
        },

        async buscarColaborador(colaboradorId) {
            this.loading = true
            this.error = null
            try {
                this.colaboradorAtual = await colaboradorService.buscarColaborador(colaboradorId)
                return { success: true, data: this.colaboradorAtual }
            } catch (error) {
                this.error = error.response?.data?.Mensagem || 'Erro ao buscar colaborador'
                console.error('Erro ao buscar colaborador:', error)
                return { success: false, error: this.error }
            } finally {
                this.loading = false
            }
        }
    }
})
