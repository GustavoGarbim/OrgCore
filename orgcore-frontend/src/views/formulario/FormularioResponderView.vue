<template>
  <div class="container">
    <div class="page-header">
      <h1>Responder Formulário</h1>
      <router-link to="/formularios" class="btn btn-secondary">← Voltar</router-link>
    </div>

    <div class="form-card">
      <LoadingSpinner v-if="loading" message="Carregando formulário..." />

      <div v-else-if="formulario">
        <div class="formulario-header">
          <h2>{{ formulario.titulo }}</h2>
          <p class="descricao">{{ formulario.descricao }}</p>
        </div>

        <form @submit.prevent="handleSubmit">
          <div class="form-group">
            <label for="colaboradorId">Seu ID de Colaborador *</label>
            <input
              id="colaboradorId"
              v-model="respostaData.colaboradorId"
              type="text"
              placeholder="GUID do seu colaborador"
              required
            />
            <p class="help-text">ID do colaborador respondendo o formulário</p>
          </div>

          <div v-for="(secao, secaoIndex) in formulario.secoes" :key="secaoIndex" class="secao-card">
            <h3>{{ secao.titulo }}</h3>

            <div v-for="(questao, questaoIndex) in secao.questoes" :key="questao.id" class="questao-item">
              <label :for="`questao-${questao.id}`" class="questao-label">
                {{ questao.enunciado }}
                <span v-if="questao.obrigatorio" class="required">*</span>
              </label>

              <!-- Texto Livre -->
              <textarea
                v-if="questao.tipo === TipoQuestao.TEXTO_LIVRE"
                :id="`questao-${questao.id}`"
                v-model="respostas[questao.id]"
                :required="questao.obrigatorio"
                rows="3"
                placeholder="Digite sua resposta"
              ></textarea>

              <!-- Nota (1-10) -->
              <div v-else-if="questao.tipo === TipoQuestao.NOTA" class="nota-input">
                <input
                  :id="`questao-${questao.id}`"
                  v-model.number="respostas[questao.id]"
                  type="number"
                  min="1"
                  max="10"
                  step="0.5"
                  :required="questao.obrigatorio"
                  placeholder="1-10"
                />
                <span class="nota-label">/ 10</span>
              </div>

              <!-- Múltipla Escolha (simulado como texto livre) -->
              <select
                v-else-if="questao.tipo === TipoQuestao.MULTIPLA_ESCOLHA"
                :id="`questao-${questao.id}`"
                v-model="respostas[questao.id]"
                :required="questao.obrigatorio"
              >
                <option value="">Selecione uma opção</option>
                <option value="Excelente">Excelente</option>
                <option value="Bom">Bom</option>
                <option value="Regular">Regular</option>
                <option value="Insatisfatório">Insatisfatório</option>
              </select>
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
            <button type="submit" class="btn btn-primary" :disabled="enviando">
              {{ enviando ? 'Enviando...' : 'Enviar Respostas' }}
            </button>
          </div>
        </form>
      </div>

      <div v-else class="empty-state">
        <p>Formulário não encontrado</p>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { useFormularioStore } from '../../stores/formulario'
import { TipoQuestao } from '../../constants/enums'
import LoadingSpinner from '../../components/common/LoadingSpinner.vue'

const route = useRoute()
const router = useRouter()
const formularioStore = useFormularioStore()

const formulario = ref(null)
const respostas = ref({})
const respostaData = ref({
  colaboradorId: ''
})

const loading = ref(false)
const enviando = ref(false)
const errorMessage = ref('')
const successMessage = ref('')

onMounted(async () => {
  loading.value = true
  
  // TODO: Buscar formulário pelo ID da rota
  // Por enquanto, vamos simular com dados mock
  formulario.value = {
    id: route.params.id,
    titulo: 'Formulário de Exemplo',
    descricao: 'Este é um formulário de exemplo para teste',
    secoes: [
      {
        titulo: 'Seção 1',
        questoes: [
          { id: '1', enunciado: 'Como você avalia sua produtividade?', tipo: TipoQuestao.NOTA, obrigatorio: true },
          { id: '2', enunciado: 'Comentários adicionais', tipo: TipoQuestao.TEXTO_LIVRE, obrigatorio: false }
        ]
      }
    ]
  }
  
  loading.value = false
})

const handleSubmit = async () => {
  errorMessage.value = ''
  successMessage.value = ''

  if (!respostaData.value.colaboradorId) {
    errorMessage.value = 'Preencha o ID do colaborador'
    return
  }

  enviando.value = true

  try {
    const data = {
      FormularioId: route.params.id,
      ColaboradorId: respostaData.value.colaboradorId,
      Respostas: Object.keys(respostas.value).map(questaoId => ({
        QuestaoId: questaoId,
        Valor: String(respostas.value[questaoId])
      }))
    }

    const result = await formularioStore.responderFormulario(data)

    if (result.success) {
      successMessage.value = 'Respostas enviadas com sucesso!'
      setTimeout(() => {
        router.push('/formularios')
      }, 1500)
    } else {
      errorMessage.value = result.error || 'Erro ao enviar respostas'
    }
  } catch (error) {
    errorMessage.value = 'Erro inesperado ao enviar respostas'
    console.error('Erro:', error)
  } finally {
    enviando.value = false
  }
}
</script>

<style scoped>
.container {
  max-width: 800px;
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

.form-card {
  background: white;
  border-radius: 12px;
  padding: 2rem;
  box-shadow: 0 1px 3px rgba(0, 0, 0, 0.1);
}

.formulario-header {
  margin-bottom: 2rem;
  padding-bottom: 1.5rem;
  border-bottom: 2px solid #e5e7eb;
}

.formulario-header h2 {
  margin: 0 0 0.5rem 0;
  color: #111827;
}

.descricao {
  margin: 0;
  color: #6b7280;
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

.form-group input {
  width: 100%;
  padding: 0.75rem 1rem;
  border: 2px solid #e5e7eb;
  border-radius: 8px;
  font-size: 1rem;
}

.help-text {
  margin-top: 0.5rem;
  font-size: 0.875rem;
  color: #6b7280;
}

.secao-card {
  background: #f9fafb;
  border-radius: 8px;
  padding: 1.5rem;
  margin-bottom: 1.5rem;
}

.secao-card h3 {
  margin: 0 0 1.5rem 0;
  color: #111827;
  font-size: 1.25rem;
}

.questao-item {
  margin-bottom: 1.5rem;
}

.questao-label {
  display: block;
  margin-bottom: 0.5rem;
  font-weight: 500;
  color: #374151;
}

.required {
  color: #ef4444;
}

textarea,
input[type="number"],
select {
  width: 100%;
  padding: 0.75rem 1rem;
  border: 2px solid #e5e7eb;
  border-radius: 8px;
  font-size: 1rem;
  font-family: inherit;
}

textarea:focus,
input[type="number"]:focus,
select:focus {
  outline: none;
  border-color: #667eea;
}

.nota-input {
  display: flex;
  align-items: center;
  gap: 0.5rem;
}

.nota-input input {
  width: 100px;
}

.nota-label {
  color: #6b7280;
  font-weight: 500;
}

.empty-state {
  text-align: center;
  padding: 3rem;
  color: #6b7280;
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
  padding-top: 1.5rem;
  border-top: 2px solid #e5e7eb;
}
</style>
