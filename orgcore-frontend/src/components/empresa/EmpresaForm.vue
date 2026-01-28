<template>
  <div class="empresa-form">
    <h2>Cadastrar Empresa</h2>
    <form @submit.prevent="handleSubmit">
      <div class="form-group">
        <label for="nomeFantasia">Nome Fantasia:</label>
        <input 
          id="nomeFantasia" 
          v-model="form.nomeFantasia" 
          type="text" 
          required 
          placeholder="Digite o nome fantasia"
        />
      </div>

      <div class="form-group">
        <label for="cnpj">CNPJ:</label>
        <input 
          id="cnpj" 
          v-model="form.cnpj" 
          type="text" 
          required 
          placeholder="00.000.000/0000-00"
        />
      </div>

      <div class="form-group">
        <label for="nomeAdmin">Nome do Administrador:</label>
        <input 
          id="nomeAdmin" 
          v-model="form.nomeAdmin" 
          type="text" 
          required 
          placeholder="Nome completo"
        />
      </div>

      <div class="form-group">
        <label for="emailAdmin">Email do Administrador:</label>
        <input 
          id="emailAdmin" 
          v-model="form.emailAdmin" 
          type="email" 
          required 
          placeholder="email@exemplo.com"
        />
      </div>

      <div class="form-actions">
        <button type="submit" :disabled="loading">
          {{ loading ? 'Cadastrando...' : 'Cadastrar Empresa' }}
        </button>
      </div>

      <div v-if="message" class="message" :class="messageType">
        {{ message }}
      </div>
    </form>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import { empresaService } from '../../services/empresaService';
import type { EmpresaDTO } from '../../types/Empresa';

const form = ref<EmpresaDTO>({
  nomeFantasia: '',
  cnpj: '',
  nomeAdmin: '',
  emailAdmin: ''
});

const loading = ref(false);
const message = ref('');
const messageType = ref<'success' | 'error'>('success');

const emit = defineEmits<{
  empresaCriada: []
}>();

const handleSubmit = async () => {
  loading.value = true;
  message.value = '';
  
  try {
    const response = await empresaService.criar(form.value);
    message.value = response.mensagem;
    messageType.value = 'success';
    
    // Limpar formulário
    form.value = {
      nomeFantasia: '',
      cnpj: '',
      nomeAdmin: '',
      emailAdmin: ''
    };
    
    emit('empresaCriada');
  } catch (error: any) {
    message.value = error.response?.data?.erro || 'Erro ao cadastrar empresa';
    messageType.value = 'error';
  } finally {
    loading.value = false;
  }
};
</script>

<style scoped>
.empresa-form {
  max-width: 500px;
  margin: 0 auto;
  padding: 2rem;
  background: #fff;
  border-radius: 8px;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
}

h2 {
  margin-bottom: 1.5rem;
  color: #2c3e50;
}

.form-group {
  margin-bottom: 1.5rem;
}

label {
  display: block;
  margin-bottom: 0.5rem;
  font-weight: 600;
  color: #34495e;
}

input {
  width: 100%;
  padding: 0.75rem;
  border: 1px solid #ddd;
  border-radius: 4px;
  font-size: 1rem;
  transition: border-color 0.3s;
}

input:focus {
  outline: none;
  border-color: #3498db;
}

.form-actions {
  margin-top: 2rem;
}

button {
  width: 100%;
  padding: 0.75rem;
  background: #3498db;
  color: white;
  border: none;
  border-radius: 4px;
  font-size: 1rem;
  font-weight: 600;
  cursor: pointer;
  transition: background 0.3s;
}

button:hover:not(:disabled) {
  background: #2980b9;
}

button:disabled {
  background: #95a5a6;
  cursor: not-allowed;
}

.message {
  margin-top: 1rem;
  padding: 0.75rem;
  border-radius: 4px;
  text-align: center;
}

.message.success {
  background: #d4edda;
  color: #155724;
  border: 1px solid #c3e6cb;
}

.message.error {
  background: #f8d7da;
  color: #721c24;
  border: 1px solid #f5c6cb;
}
</style>
