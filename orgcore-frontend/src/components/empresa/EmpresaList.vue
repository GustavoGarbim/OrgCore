<template>
  <div class="empresa-list">
    <h2>Empresas Cadastradas</h2>
    
    <div v-if="loading" class="loading">Carregando...</div>
    
    <div v-else-if="empresas.length === 0" class="empty">
      Nenhuma empresa cadastrada
    </div>
    
    <div v-else class="grid">
      <div v-for="empresa in empresas" :key="empresa.id" class="card">
        <h3>{{ empresa.nomeFantasia }}</h3>
        <p><strong>CNPJ:</strong> {{ formatCnpj(empresa.cnpj) }}</p>
        <p><strong>Status:</strong> 
          <span :class="empresa.ativo ? 'ativo' : 'inativo'">
            {{ empresa.ativo ? 'Ativa' : 'Inativa' }}
          </span>
        </p>
      </div>
    </div>
    
    <button @click="carregar" class="refresh-btn">Atualizar Lista</button>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { empresaService } from '../../services/empresaService';
import type { Empresa } from '../../types/Empresa';

const empresas = ref<Empresa[]>([]);
const loading = ref(false);

const carregar = async () => {
  loading.value = true;
  try {
    empresas.value = await empresaService.obterTodas();
  } catch (error) {
    console.error('Erro ao carregar empresas:', error);
  } finally {
    loading.value = false;
  }
};

const formatCnpj = (cnpj: string) => {
  if (!cnpj || cnpj.length !== 14) return cnpj;
  return cnpj.replace(/(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})/, '$1.$2.$3/$4-$5');
};

onMounted(carregar);

defineExpose({ carregar });
</script>

<style scoped>
.empresa-list {
  padding: 2rem;
}

h2 {
  margin-bottom: 2rem;
  color: #2c3e50;
}

.loading, .empty {
  text-align: center;
  padding: 2rem;
  color: #7f8c8d;
}

.grid {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(280px, 1fr));
  gap: 1.5rem;
  margin-bottom: 2rem;
}

.card {
  background: #fff;
  padding: 1.5rem;
  border-radius: 8px;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
  transition: transform 0.2s, box-shadow 0.2s;
}

.card:hover {
  transform: translateY(-4px);
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.15);
}

.card h3 {
  margin: 0 0 1rem 0;
  color: #2c3e50;
}

.card p {
  margin: 0.5rem 0;
  color: #555;
}

.ativo {
  color: #27ae60;
  font-weight: 600;
}

.inativo {
  color: #e74c3c;
  font-weight: 600;
}

.refresh-btn {
  padding: 0.75rem 1.5rem;
  background: #3498db;
  color: white;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  font-size: 1rem;
  transition: background 0.3s;
}

.refresh-btn:hover {
  background: #2980b9;
}
</style>
