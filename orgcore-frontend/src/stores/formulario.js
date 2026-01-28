import { defineStore } from 'pinia'
import { formularioService } from '../services/formularioService'

export const useFormularioStore = defineStore('formulario', {
    state: () => ({
        formularios: [],
        formularioAtual: null,
        avaliacoes: [],
        avaliacaoAtual: null,
        loading: false,
        error: null
    }),

    getters: {
        getFormularioById: (state) => (id) => {
            return state.formularios.find(form => form.id === id)
        }
    },

    actions: {
        async listarFormulariosPorEmpresa(empresaId) {
            this.loading = true
            this.error = null
            try {
                this.formularios = await formularioService.listarFormulariosPorEmpresa(empresaId)
                return { success: true }
            } catch (error) {
                this.error = error.response?.data?.Mensagem || 'Erro ao listar formulários'
                console.error('Erro ao listar formulários:', error)
                return { success: false, error: this.error }
            } finally {
                this.loading = false
            }
        },

        async criarFormulario(data) {
            this.loading = true
            this.error = null
            try {
                const result = await formularioService.criarFormulario(data)
                await this.listarFormulariosPorEmpresa(data.EmpresaId) // Atualiza lista
                return { success: true, data: result }
            } catch (error) {
                this.error = error.response?.data?.Mensagem || 'Erro ao criar formulário'
                console.error('Erro ao criar formulário:', error)
                return { success: false, error: this.error }
            } finally {
                this.loading = false
            }
        },

        async responderFormulario(data) {
            this.loading = true
            this.error = null
            try {
                const result = await formularioService.responderFormulario(data)
                return { success: true, data: result }
            } catch (error) {
                this.error = error.response?.data?.Mensagem || 'Erro ao responder formulário'
                console.error('Erro ao responder formulário:', error)
                return { success: false, error: this.error }
            } finally {
                this.loading = false
            }
        },

        async obterRelatorio(id) {
            this.loading = true
            this.error = null
            try {
                this.avaliacaoAtual = await formularioService.obterRelatorio(id)
                return { success: true, data: this.avaliacaoAtual }
            } catch (error) {
                this.error = error.response?.data?.Mensagem || 'Erro ao obter relatório'
                console.error('Erro ao obter relatório:', error)
                return { success: false, error: this.error }
            } finally {
                this.loading = false
            }
        },

        setFormularioAtual(formulario) {
            this.formularioAtual = formulario
        }
    }
})
