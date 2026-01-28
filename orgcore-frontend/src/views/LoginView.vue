<template>
  <div class="login-container">
    <div class="login-card">
      <div class="login-header">
        <h1>OrgCore</h1>
        <p>Sistema de Gestão de Empresas e Avaliações</p>
      </div>
      
      <form @submit.prevent="handleLogin" class="login-form">
        <div class="form-group">
          <label for="cpf">CPF</label>
          <input
            id="cpf"
            v-model="cpf"
            type="text"
            placeholder="000.000.000-00"
            maxlength="14"
            @input="formatCPFInput"
            required
          />
          <span v-if="cpfError" class="error-message">{{ cpfError }}</span>
        </div>
        
        <button type="submit" class="login-button" :disabled="loading">
          <span v-if="!loading">Entrar</span>
          <span v-else>Entrando...</span>
        </button>
        
        <div v-if="errorMessage" class="alert alert-error">
          {{ errorMessage }}
        </div>
      </form>
      
      <div class="login-footer">
        <p>Primeiro acesso? Entre com seu CPF cadastrado.</p>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import { useAuthStore } from '../stores/auth'
import { validarCPF, formatarCPF } from '../utils/validators'

const router = useRouter()
const authStore = useAuthStore()

const cpf = ref('')
const cpfError = ref('')
const errorMessage = ref('')
const loading = ref(false)

const formatCPFInput = (event) => {
  const value = event.target.value.replace(/\D/g, '')
  cpf.value = formatarCPF(value)
  cpfError.value = ''
}

const handleLogin = async () => {
  // Limpar mensagens de erro
  cpfError.value = ''
  errorMessage.value = ''

  // Validar CPF
  const cpfLimpo = cpf.value.replace(/\D/g, '')
  if (!validarCPF(cpfLimpo)) {
    cpfError.value = 'CPF inválido'
    return
  }

  loading.value = true

  try {
    const result = await authStore.login(cpfLimpo)
    
    if (result.success) {
      router.push('/dashboard')
    } else {
      errorMessage.value = result.error || 'Erro ao fazer login'
    }
  } catch (error) {
    errorMessage.value = 'Erro inesperado ao fazer login'
    console.error('Erro no login:', error)
  } finally {
    loading.value = false
  }
}
</script>

<style scoped>
.login-container {
  min-height: 100vh;
  display: flex;
  align-items: center;
  justify-content: center;
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
  padding: 1rem;
}

.login-card {
  background: white;
  border-radius: 12px;
  box-shadow: 0 10px 40px rgba(0, 0, 0, 0.2);
  width: 100%;
  max-width: 420px;
  overflow: hidden;
}

.login-header {
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
  color: white;
  padding: 2rem;
  text-align: center;
}

.login-header h1 {
  margin: 0 0 0.5rem 0;
  font-size: 2rem;
  font-weight: 700;
}

.login-header p {
  margin: 0;
  opacity: 0.9;
  font-size: 0.875rem;
}

.login-form {
  padding: 2rem;
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

.login-button {
  width: 100%;
  padding: 0.875rem;
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
  color: white;
  border: none;
  border-radius: 8px;
  font-size: 1rem;
  font-weight: 600;
  cursor: pointer;
  transition: transform 0.2s, box-shadow 0.2s;
}

.login-button:hover:not(:disabled) {
  transform: translateY(-2px);
  box-shadow: 0 6px 20px rgba(102, 126, 234, 0.4);
}

.login-button:disabled {
  opacity: 0.6;
  cursor: not-allowed;
}

.alert {
  padding: 0.75rem 1rem;
  border-radius: 8px;
  margin-top: 1rem;
}

.alert-error {
  background-color: #fee2e2;
  color: #991b1b;
  border: 1px solid #fecaca;
}

.login-footer {
  padding: 1.5rem 2rem;
  background-color: #f9fafb;
  text-align: center;
  border-top: 1px solid #e5e7eb;
}

.login-footer p {
  margin: 0;
  color: #6b7280;
  font-size: 0.875rem;
}
</style>
