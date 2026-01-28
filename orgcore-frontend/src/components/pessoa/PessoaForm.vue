<template>
  <div class="pessoa-form">
    <h2>Cadastrar Pessoa</h2>
    <form @submit.prevent="handleSubmit">
      <div class="form-group">
        <label for="nome">Nome Completo:</label>
        <input 
          id="nome" 
          v-model="form.nome" 
          type="text" 
          required 
          placeholder="Digite o nome completo"
        />
      </div>

      <div class="form-group">
        <label for="cpf">CPF:</label>
        <input 
          id="cpf" 
          v-model="form.cpf" 
          type="text" 
          required 
          placeholder="000.000.000-00"
        />
      </div>

      <div class="form-group">
        <label for="email">Email:</label>
        <input 
          id="email" 
          v-model="form.emailPessoal" 
          type="email" 
          required 
          placeholder="email@exemplo.com"
        />
      </div>

      <div class="form-actions">
        <button type="submit" :disabled="loading">
          {{ loading ? 'Cadastrando...' : 'Cadastrar Pessoa' }}
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
import { pessoaService } from '../../services/pessoaService';
import type { CriarPessoaDto } from '../../types/Pessoa';

const form = ref<CriarPessoaDto>({
  nome: '',
  cpf: '',
  emailPessoal: ''
});

const loading = ref(false);
const message = ref('');
const messageType = ref<'success' | 'error'>('success');

const emit = defineEmits<{
  pessoaCriada: []
}>();

const handleSubmit = async () => {
  loading.value = true;
  message.value = '';
  
  try {
    const response = await pessoaService.registrar(form.value);
    message.value = response.mensagem;
    messageType.value = 'success';
    
    form.value = {
      nome: '',
      cpf: '',
      emailPessoal: ''
    };
    
    emit('pessoaCriada');
  } catch (error: any) {
    message.value = error.response?.data?.erro || 'Erro ao cadastrar pessoa';
    messageType.value = 'error';
  } finally {
    loading.value = false;
  }
};
</script>

<style scoped>
.pessoa-form {
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
  background: #27ae60;
  color: white;
  border: none;
  border-radius: 4px;
  font-size: 1rem;
  font-weight: 600;
  cursor: pointer;
  transition: background 0.3s;
}

button:hover:not(:disabled) {
  background: #229954;
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
