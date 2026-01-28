<template>
  <div class="colaborador-form">
    <h2>Vincular Colaborador</h2>
    <form @submit.prevent="handleSubmit">
      <div class="form-group">
        <label for="empresaId">Empresa:</label>
        <select id="empresaId" v-model="form.empresaId" required>
          <option value="">Selecione uma empresa</option>
          <option v-for="empresa in empresas" :key="empresa.id" :value="empresa.id">
            {{ empresa.nomeFantasia }}
          </option>
        </select>
      </div>

      <div class="form-group">
        <label for="pessoaId">Pessoa (ID):</label>
        <input 
          id="pessoaId" 
          v-model="form.pessoaId" 
          type="text" 
          required 
          placeholder="Digite o ID da pessoa"
        />
        <small>Você pode obter o ID ao cadastrar uma pessoa</small>
      </div>

      <div class="form-actions">
        <button type="submit" :disabled="loading">
          {{ loading ? 'Vinculando...' : 'Vincular Colaborador' }}
        </button>
      </div>

      <div v-if="message" class="message" :class="messageType">
        {{ message }}
      </div>
    </form>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { colaboradorService } from '../../services/colaboradorService';
import { empresaService } from '../../services/empresaService';
import type { CadastrarColaboradorDTO } from '../../types/Colaborador';
import type { Empresa } from '../../types/Empresa';

const form = ref<CadastrarColaboradorDTO>({
  empresaId: '',
  pessoaId: ''
});

const empresas = ref<Empresa[]>([]);
const loading = ref(false);
const message = ref('');
const messageType = ref<'success' | 'error'>('success');

const emit = defineEmits<{
  colaboradorVinculado: []
}>();

const carregarEmpresas = async () => {
  try {
    empresas.value = await empresaService.obterTodas();
  } catch (error) {
    console.error('Erro ao carregar empresas:', error);
  }
};

const handleSubmit = async () => {
  loading.value = true;
  message.value = '';
  
  try {
    const response = await colaboradorService.vincular(form.value);
    message.value = typeof response === 'string' ? response : 'Colaborador vinculado com sucesso!';
    messageType.value = 'success';
    
    form.value = {
      empresaId: '',
      pessoaId: ''
    };
    
    emit('colaboradorVinculado');
  } catch (error: any) {
    message.value = error.response?.data?.erro || 'Erro ao vincular colaborador';
    messageType.value = 'error';
  } finally {
    loading.value = false;
  }
};

onMounted(carregarEmpresas);
</script>

<style scoped>
.colaborador-form {
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

input, select {
  width: 100%;
  padding: 0.75rem;
  border: 1px solid #ddd;
  border-radius: 4px;
  font-size: 1rem;
  transition: border-color 0.3s;
}

input:focus, select:focus {
  outline: none;
  border-color: #3498db;
}

small {
  display: block;
  margin-top: 0.25rem;
  color: #7f8c8d;
  font-size: 0.875rem;
}

.form-actions {
  margin-top: 2rem;
}

button {
  width: 100%;
  padding: 0.75rem;
  background: #9b59b6;
  color: white;
  border: none;
  border-radius: 4px;
  font-size: 1rem;
  font-weight: 600;
  cursor: pointer;
  transition: background 0.3s;
}

button:hover:not(:disabled) {
  background: #8e44ad;
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
