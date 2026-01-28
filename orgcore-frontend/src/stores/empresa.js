import { defineStore } from 'pinia'
import { empresaService } from '../services/empresaService'

export const useEmpresaStore = defineStore('empresa', {
    state: () => ({
        empresas: [],
        empresaAtual: null,
        loading: false,
        error: null
    }),

    getters: {
        getEmpresaById: (state) => (id) => {
            return state.empresas.find(emp => emp.id === id)
        }
    },

    actions: {
        async listarEmpresas() {
            this.loading = true
            this.error = null
            try {
                this.empresas = await empresaService.listarEmpresas()
                return { success: true }
            } catch (error) {
                this.error = error.response?.data?.Mensagem || 'Erro ao listar empresas'
                console.error('Erro ao listar empresas:', error)
                return { success: false, error: this.error }
            } finally {
                this.loading = false
            }
        },

        async criarEmpresa(data) {
            this.loading = true
            this.error = null
            try {
                const result = await empresaService.criarEmpresa(data)
                await this.listarEmpresas() // Atualiza lista
                return { success: true, data: result }
            } catch (error) {
                this.error = error.response?.data?.Mensagem || 'Erro ao criar empresa'
                console.error('Erro ao criar empresa:', error)
                return { success: false, error: this.error }
            } finally {
                this.loading = false
            }
        },

        async atribuirCargo(empresaId, colaboradorId, cargo) {
            this.loading = true
            this.error = null
            try {
                const data = { ColaboradorId: colaboradorId, Cargo: cargo }
                const result = await empresaService.atribuirCargo(empresaId, data)
                return { success: true, data: result }
            } catch (error) {
                this.error = error.response?.data?.Mensagem || 'Erro ao atribuir cargo'
                console.error('Erro ao atribuir cargo:', error)
                return { success: false, error: this.error }
            } finally {
                this.loading = false
            }
        },

        setEmpresaAtual(empresa) {
            this.empresaAtual = empresa
        }
    }
})
