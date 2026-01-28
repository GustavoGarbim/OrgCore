<template>
  <div class="container">
    <div class="page-header">
      <h1>Registrar Nova Pessoa</h1>
      <router-link to="/pessoas" class="btn btn-secondary">← Voltar</router-link>
    </div>

    <div class="form-card">
      <form @submit.prevent="handleSubmit">
        <div class="form-group">
          <label for="nome">Nome Completo *</label>
          <input
            id="nome"
            v-model="formData.nome"
            type="text"
            placeholder="Nome completo da pessoa"
            required
          />
        </div>

        <div class="form-group">
          <label for="cpf">CPF *</label>
          <input
            id="cpf"
            v-model="formData.cpf"
            type="text"
            placeholder="000.000.000-00"
            maxlength="14"
            @input="formatCPFInput"
            required
          />
          <span v-if="cpfError" class="error-message">{{ cpfError }}</span>
        </div>

        <div class="form-group">
          <label for="emailPessoal">Email Pessoal *</label>
          <input
            id="emailPessoal"
            v-model="formData.emailPessoal"
            type="email"
            placeholder="email@exemplo.com"
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
          <router-link to="/pessoas" class="btn btn-secondary">Cancelar</router-link>
          <button type="submit" class="btn btn-primary" :disabled="loading">
            {{ loading ? 'Salvando...' : 'Registrar Pessoa' }}
          </button>
        </div>
      </form>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import { usePessoaStore } from '../../stores/pessoa'
import { validarCPF, validarEmail, formatarCPF } from '../../utils/validators'

const router = useRouter()
const pessoaStore = usePessoaStore()

const formData = ref({
  nome: '',
  cpf: '',
  emailPessoal: ''
})

const cpfError = ref('')
const emailError = ref('')
const errorMessage = ref('')
const successMessage = ref('')
const loading = ref(false)

const formatCPFInput = (event) => {
  const value = event.target.value.replace(/\D/g, '')
  formData.value.cpf = formatarCPF(value)
  cpfError.value = ''
}

const handleSubmit = async () => {
  cpfError.value = ''
  emailError.value = ''
  errorMessage.value = ''
  successMessage.value = ''

  const cpfLimpo = formData.value.cpf.replace(/\D/g, '')
  if (!validarCPF(cpfLimpo)) {
    cpfError.value = 'CPF inválido'
    return
  }

  if (!validarEmail(formData.value.emailPessoal)) {
    emailError.value = 'Email inválido'
    return
  }

  loading.value = true

  try {
    const data = {
      Nome: formData.value.nome,
      Cpf: cpfLimpo,
      EmailPessoal: formData.value.emailPessoal
    }

    const result = await pessoaStore.registrarPessoa(data)

    if (result.success) {
      successMessage.value = 'Pessoa registrada com sucesso!'
      setTimeout(() => {
        router.push('/pessoas')
      }, 1500)
    } else {
      errorMessage.value = result.error || 'Erro ao registrar pessoa'
    }
  } catch (error) {
    errorMessage.value = 'Erro inesperado ao registrar pessoa'
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
