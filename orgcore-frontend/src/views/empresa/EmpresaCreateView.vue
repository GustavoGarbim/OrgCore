<template>
  <div class="container">
    <div class="page-header">
      <h1>Criar Nova Empresa</h1>
      <router-link to="/empresas" class="btn btn-secondary">← Voltar</router-link>
    </div>

    <div class="form-card">
      <form @submit.prevent="handleSubmit">
        <div class="form-group">
          <label for="nomeFantasia">Nome Fantasia *</label>
          <input
            id="nomeFantasia"
            v-model="formData.nomeFantasia"
            type="text"
            placeholder="Ex: Tech Solutions LTDA"
            required
          />
        </div>

        <div class="form-group">
          <label for="cnpj">CNPJ *</label>
          <input
            id="cnpj"
            v-model="formData.cnpj"
            type="text"
            placeholder="00.000.000/0000-00"
            maxlength="18"
            @input="formatCNPJInput"
            required
          />
          <span v-if="cnpjError" class="error-message">{{ cnpjError }}</span>
        </div>

        <div class="form-group">
          <label for="nomeAdmin">Nome do Administrador *</label>
          <input
            id="nomeAdmin"
            v-model="formData.nomeAdmin"
            type="text"
            placeholder="Nome completo do administrador"
            required
          />
        </div>

        <div class="form-group">
          <label for="emailAdmin">Email do Administrador *</label>
          <input
            id="emailAdmin"
            v-model="formData.emailAdmin"
            type="email"
            placeholder="admin@empresa.com"
            required
          />
          <span v-if="emailError" class="error-message">{{ emailError }}</span>
        </div>

        <div v-if="errorMessage" class="alert alert-error">
          {{ errorMessage }}
        </div>

        <div v-if="successMessage" class="alert alert-success">
          {{ successMessage }}
        </div>

        <div class="form-actions">
          <router-link to="/empresas" class="btn btn-secondary">Cancelar</router-link>
          <button type="submit" class="btn btn-primary" :disabled="loading">
            {{ loading ? 'Salvando...' : 'Criar Empresa' }}
          </button>
        </div>
      </form>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import { useEmpresaStore } from '../../stores/empresa'
import { validarCNPJ, validarEmail, formatarCNPJ } from '../../utils/validators'

const router = useRouter()
const empresaStore = useEmpresaStore()

const formData = ref({
  nomeFantasia: '',
  cnpj: '',
  nomeAdmin: '',
  emailAdmin: ''
})

const cnpjError = ref('')
const emailError = ref('')
const errorMessage = ref('')
const successMessage = ref('')
const loading = ref(false)

const formatCNPJInput = (event) => {
  const value = event.target.value.replace(/\D/g, '')
  formData.value.cnpj = formatarCNPJ(value)
  cnpjError.value = ''
}

const handleSubmit = async () => {
  // Limpar mensagens
  cnpjError.value = ''
  emailError.value = ''
  errorMessage.value = ''
  successMessage.value = ''

  // Validações
  const cnpjLimpo = formData.value.cnpj.replace(/\D/g, '')
  if (!validarCNPJ(cnpjLimpo)) {
    cnpjError.value = 'CNPJ inválido'
    return
  }

  if (!validarEmail(formData.value.emailAdmin)) {
    emailError.value = 'Email inválido'
    return
  }

  loading.value = true

  try {
    const data = {
      NomeFantasia: formData.value.nomeFantasia,
      Cnpj: cnpjLimpo,
      NomeAdmin: formData.value.nomeAdmin,
      EmailAdmin: formData.value.emailAdmin
    }

    const result = await empresaStore.criarEmpresa(data)

    if (result.success) {
      successMessage.value = 'Empresa criada com sucesso!'
      setTimeout(() => {
        router.push('/empresas')
      }, 1500)
    } else {
      errorMessage.value = result.error || 'Erro ao criar empresa'
    }
  } catch (error) {
    errorMessage.value = 'Erro inesperado ao criar empresa'
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

.form-group input {
  width: 100%;
  padding: 0.75rem 1rem;
  border: 2px solid #e5e7eb;
  border-radius: 8px;
  font-size: 1rem;
  transition: border-color 0.3s;
}

.form-group input:focus {
  outline: none;
  border-color: #667eea;
}

.error-message {
  display: block;
  color: #ef4444;
  font-size: 0.875rem;
  margin-top: 0.25rem;
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
