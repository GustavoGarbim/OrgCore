import { defineStore } from 'pinia'
import { pessoaService } from '../services/pessoaService'

export const usePessoaStore = defineStore('pessoa', {
    state: () => ({
        pessoas: [],
        pessoaAtual: null,
        loading: false,
        error: null
    }),

    actions: {
        async registrarPessoa(data) {
            this.loading = true
            this.error = null
            try {
                const result = await pessoaService.registrarPessoa(data)
                return { success: true, data: result }
            } catch (error) {
                this.error = error.response?.data?.Erro || error.response?.data?.Mensagem || 'Erro ao registrar pessoa'
                console.error('Erro ao registrar pessoa:', error)
                return { success: false, error: this.error }
            } finally {
                this.loading = false
            }
        },

        async buscarPessoa(id) {
            this.loading = true
            this.error = null
            try {
                this.pessoaAtual = await pessoaService.buscarPessoa(id)
                return { success: true, data: this.pessoaAtual }
            } catch (error) {
                this.error = error.response?.data?.Mensagem || 'Erro ao buscar pessoa'
                console.error('Erro ao buscar pessoa:', error)
                return { success: false, error: this.error }
            } finally {
                this.loading = false
            }
        }
    }
})
