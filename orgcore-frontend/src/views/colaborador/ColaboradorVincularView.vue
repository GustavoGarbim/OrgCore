<template>
  <div class="container">
    <div class="page-header">
      <h1>Vincular Colaborador</h1>
      <router-link to="/colaboradores" class="btn btn-secondary">← Voltar</router-link>
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
          <label for="pessoaId">Pessoa *</label>
          <input
            id="pessoaId"
            v-model="formData.pessoaId"
            type="text"
            placeholder="ID da pessoa (GUID)"
            required
          />
          <p class="help-text">Cole o ID da pessoa que deseja vincular. Para obter o ID, registre a pessoa primeiro.</p>
        </div>

        <div v-if="errorMessage" class="alert alert-error">
          {{ errorMessage }}
        </div>

        <div v-if="successMessage" class="alert alert-success">
          {{ successMessage }}
        </div>

        <div class="form-actions">
          <router-link to="/colaboradores" class="btn btn-secondary">Cancelar</router-link>
          <button type="submit" class="btn btn-primary" :disabled="loading">
            {{ loading ? 'Vinculando...' : 'Vincular Colaborador' }}
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
import { useColaboradorStore } from '../../stores/colaborador'

const router = useRouter()
const empresaStore = useEmpresaStore()
const colaboradorStore = useColaboradorStore()

const formData = ref({
  empresaId: null,
  pessoaId: ''
})

const errorMessage = ref('')
const successMessage = ref('')
const loading = ref(false)

const empresas = computed(() => empresaStore.empresas)

onMounted(async () => {
  await empresaStore.listarEmpresas()
})

const handleSubmit = async () => {
  errorMessage.value = ''
  successMessage.value = ''

  if (!formData.value.empresaId || !formData.value.pessoaId) {
    errorMessage.value = 'Preencha todos os campos'
    return
  }

  loading.value = true

  try {
    const result = await colaboradorStore.vincularColaborador(
      formData.value.empresaId,
      formData.value.pessoaId
    )

    if (result.success) {
      successMessage.value = 'Colaborador vinculado com sucesso!'
      setTimeout(() => {
        router.push('/colaboradores')
      }, 1500)
    } else {
      errorMessage.value = result.error || 'Erro ao vincular colaborador'
    }
  } catch (error) {
    errorMessage.value = 'Erro inesperado ao vincular colaborador'
    console.error('Erro:', error)
  } finally {
    loading.value = false
  }
}
</script>

<style scoped>
.container {
  max-width: 600px;
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
.form-group select {
  width: 100%;
  padding: 0.75rem 1rem;
  border: 2px solid #e5e7eb;
  border-radius: 8px;
  font-size: 1rem;
  transition: border-color 0.3s;
}

.form-group input:focus,
.form-group select:focus {
  outline: none;
  border-color: #667eea;
}

.help-text {
  margin-top: 0.5rem;
  font-size: 0.875rem;
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
}
</style>
