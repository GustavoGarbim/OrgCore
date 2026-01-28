<template>
  <div class="container">
    <div class="page-header">
      <h1>Criar Formulário</h1>
      <router-link to="/formularios" class="btn btn-secondary">← Voltar</router-link>
    </div>

    <div class="form-card">
      <form @submit.prevent="handleSubmit">
        <div class="form-group">
          <label for="empresaId">Empresa *</label>
          <select id="empresaId" v-model="formData.empresaId" required>
            <option :value="null">Selecione uma empresa</option>
            <option v-for="empresa in empresas" :key="empresa.id" :value="empresa.id">
              {{ empresa.nomeFantasia }}
            </option>
          </select>
        </div>

        <div class="form-group">
          <label for="titulo">Título do Formulário *</label>
          <input
            id="titulo"
            v-model="formData.titulo"
            type="text"
            placeholder="Ex: Avaliação de Desempenho Q1 2026"
            required
          />
        </div>

        <div class="form-group">
          <label for="descricao">Descrição</label>
          <textarea
            id="descricao"
            v-model="formData.descricao"
            placeholder="Descreva o objetivo deste formulário"
            rows="3"
          ></textarea>
        </div>

        <div class="form-group">
          <label>ID do Usuário Criador *</label>
          <input
            v-model="formData.idUsuarioCriador"
            type="text"
            placeholder="GUID do usuário criador"
            required
          />
          <p class="help-text">ID do usuário que está criando o formulário</p>
        </div>

        <!-- Seções e Questões -->
        <div class="secoes-section">
          <div class="section-header">
            <h3>Seções e Questões</h3>
            <button type="button" class="btn btn-secondary-small" @click="adicionarSecao">
              + Adicionar Seção
            </button>
          </div>

          <div v-for="(secao, secaoIndex) in formData.secoes" :key="secaoIndex" class="secao-card">
            <div class="secao-header">
              <input
                v-model="secao.titulo"
                type="text"
                placeholder="Título da Seção"
                class="secao-titulo"
                required
              />
              <button type="button" class="btn-icon" @click="removerSecao(secaoIndex)">
                🗑️
              </button>
            </div>

            <div class="questoes-list">
              <div v-for="(questao, questaoIndex) in secao.questoes" :key="questaoIndex" class="questao-item">
                <input
                  v-model="questao.enunciado"
                  type="text"
                  placeholder="Enunciado da questão"
                  required
                />
                <select v-model="questao.tipo" required>
                  <option :value="TipoQuestao.TEXTO_LIVRE">{{ TipoQuestaoLabels[TipoQuestao.TEXTO_LIVRE] }}</option>
                  <option :value="TipoQuestao.NOTA">{{ TipoQuestaoLabels[TipoQuestao.NOTA] }}</option>
                  <option :value="TipoQuestao.MULTIPLA_ESCOLHA">{{ TipoQuestaoLabels[TipoQuestao.MULTIPLA_ESCOLHA] }}</option>
                </select>
                <label class="checkbox-label">
                  <input v-model="questao.obrigatorio" type="checkbox" />
                  Obrigatória
                </label>
                <button type="button" class="btn-icon" @click="removerQuestao(secaoIndex, questaoIndex)">
                  ✕
                </button>
              </div>
            </div>

            <button type="button" class="btn btn-secondary-small" @click="adicionarQuestao(secaoIndex)">
              + Adicionar Questão
            </button>
          </div>
        </div>

        <div v-if="errorMessage" class="alert alert-error">
          {{ errorMessage }}
        </div>

        <div v-if="successMessage" class="alert alert-success">
          {{ successMessage }}
        </div>

        <div class="form-actions">
          <router-link to="/formularios" class="btn btn-secondary">Cancelar</router-link>
          <button type="submit" class="btn btn-primary" :disabled="loading">
            {{ loading ? 'Criando...' : 'Criar Formulário' }}
          </button>
        </div>
      </form>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted, computed } from 'vue'
import { useRouter } from 'vue-router'
import { useEmpresaStore } from '../../stores/empresa'
import { useFormularioStore } from '../../stores/formulario'
import { TipoQuestao, TipoQuestaoLabels } from '../../constants/enums'

const router = useRouter()
const empresaStore = useEmpresaStore()
const formularioStore = useFormularioStore()

const formData = ref({
  empresaId: null,
  titulo: '',
  descricao: '',
  idUsuarioCriador: '',
  secoes: []
})

const errorMessage = ref('')
const successMessage = ref('')
const loading = ref(false)

const empresas = computed(() => empresaStore.empresas)

onMounted(async () => {
  await empresaStore.listarEmpresas()
})

const adicionarSecao = () => {
  formData.value.secoes.push({
    titulo: '',
    ordem: formData.value.secoes.length + 1,
    questoes: []
  })
}

const removerSecao = (index) => {
  formData.value.secoes.splice(index, 1)
  // Reordenar
  formData.value.secoes.forEach((secao, i) => {
    secao.ordem = i + 1
  })
}

const adicionarQuestao = (secaoIndex) => {
  formData.value.secoes[secaoIndex].questoes.push({
    enunciado: '',
    tipo: TipoQuestao.TEXTO_LIVRE,
    obrigatorio: false
  })
}

const removerQuestao = (secaoIndex, questaoIndex) => {
  formData.value.secoes[secaoIndex].questoes.splice(questaoIndex, 1)
}

const handleSubmit = async () => {
  errorMessage.value = ''
  successMessage.value = ''

  if (!formData.value.empresaId || !formData.value.titulo || !formData.value.idUsuarioCriador) {
    errorMessage.value = 'Preencha todos os campos obrigatórios'
    return
  }

  if (formData.value.secoes.length === 0) {
    errorMessage.value = 'Adicione pelo menos uma seção'
    return
  }

  loading.value = true

  try {
    const data = {
      EmpresaId: formData.value.empresaId,
      Titulo: formData.value.titulo,
      Descricao: formData.value.descricao,
      IdUsuarioCriador: formData.value.idUsuarioCriador,
      Secoes: formData.value.secoes.map(secao => ({
        Titulo: secao.titulo,
        Ordem: secao.ordem,
        Questoes: secao.questoes.map(questao => ({
          Enunciado: questao.enunciado,
          Tipo: questao.tipo,
          Obrigatorio: questao.obrigatorio
        }))
      }))
    }

    const result = await formularioStore.criarFormulario(data)

    if (result.success) {
      successMessage.value = 'Formulário criado com sucesso!'
      setTimeout(() => {
        router.push('/formularios')
      }, 1500)
    } else {
      errorMessage.value = result.error || 'Erro ao criar formulário'
    }
  } catch (error) {
    errorMessage.value = 'Erro inesperado ao criar formulário'
    console.error('Erro:', error)
  } finally {
    loading.value = false
  }
}
</script>

<style scoped>
.container {
  max-width: 900px;
  margin: 0 auto;
  padding: 0 2rem;
}

.page-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 2rem;
}

.page-header h1 {
  font-size: 1.75rem;
  color: #111827;
  margin: 0;
}

.btn {
  padding: 0.75rem 1.5rem;
  border-radius: 8px;
  font-weight: 600;
  text-decoration: none;
  display: inline-block;
  transition: all 0.2s;
  border: none;
  cursor: pointer;
}

.btn-primary {
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
  color: white;
}

.btn-primary:hover:not(:disabled) {
  transform: translateY(-2px);
  box-shadow: 0 6px 20px rgba(102, 126, 234, 0.4);
}

.btn-primary:disabled {
  opacity: 0.6;
  cursor: not-allowed;
}

.btn-secondary {
  background: #f3f4f6;
  color: #374151;
}

.btn-secondary:hover {
  background: #e5e7eb;
}

.btn-secondary-small {
  background: #f3f4f6;
  color: #374151;
  padding: 0.5rem 1rem;
  font-size: 0.875rem;
  border-radius: 6px;
  border: none;
  cursor: pointer;
}

.btn-icon {
  background: none;
  border: none;
  cursor: pointer;
  font-size: 1.25rem;
  padding: 0.25rem;
}

.form-card {
  background: white;
  border-radius: 12px;
  padding: 2rem;
  box-shadow: 0 1px 3px rgba(0, 0, 0, 0.1);
}

.form-group {
  margin-bottom: 1.5rem;
}

.form-group label {
  display: block;
  margin-bottom: 0.5rem;
  font-weight: 600;
  color: #374151;
}

.form-group input,
.form-group select,
.form-group textarea {
  width: 100%;
  padding: 0.75rem 1rem;
  border: 2px solid #e5e7eb;
  border-radius: 8px;
  font-size: 1rem;
  transition: border-color 0.3s;
}

.form-group input:focus,
.form-group select:focus,
.form-group textarea:focus {
  outline: none;
  border-color: #667eea;
}

.help-text {
  margin-top: 0.5rem;
  font-size: 0.875rem;
  color: #6b7280;
}

.secoes-section {
  margin: 2rem 0;
  padding: 1.5rem;
  background: #f9fafb;
  border-radius: 8px;
}

.section-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 1.5rem;
}

.section-header h3 {
  margin: 0;
  color: #111827;
}

.secao-card {
  background: white;
  border-radius: 8px;
  padding: 1.5rem;
  margin-bottom: 1rem;
  box-shadow: 0 1px 3px rgba(0, 0, 0, 0.1);
}

.secao-header {
  display: flex;
  gap: 0.5rem;
  margin-bottom: 1rem;
}

.secao-titulo {
  flex: 1;
  font-weight: 600;
}

.questoes-list {
  margin-bottom: 1rem;
}

.questao-item {
  display: grid;
  grid-template-columns: 2fr 1fr auto auto;
  gap: 0.5rem;
  align-items: center;
  margin-bottom: 0.5rem;
}

.checkbox-label {
  display: flex;
  align-items: center;
  gap: 0.25rem;
  font-size: 0.875rem;
  font-weight: normal;
  margin: 0;
}

.alert {
  padding: 0.75rem 1rem;
  border-radius: 8px;
  margin-bottom: 1rem;
}

.alert-error {
  background-color: #fee2e2;
  color: #991b1b;
  border: 1px solid #fecaca;
}

.alert-success {
  background-color: #d1fae5;
  color: #065f46;
  border: 1px solid #6ee7b7;
}

.form-actions {
  display: flex;
  gap: 1rem;
  justify-content: flex-end;
  margin-top: 2rem;
}
</style>
