<template>
  <div class="empresas-view">
    <h1>Gestão de Empresas</h1>
    
    <div class="tabs">
      <button 
        :class="{ active: activeTab === 'cadastrar' }" 
        @click="activeTab = 'cadastrar'"
      >
        Cadastrar
      </button>
      <button 
        :class="{ active: activeTab === 'listar' }" 
        @click="activeTab = 'listar'"
      >
        Listar
      </button>
    </div>
    
    <div class="content">
      <EmpresaForm v-if="activeTab === 'cadastrar'" @empresa-criada="handleEmpresaCriada" />
      <EmpresaList v-if="activeTab === 'listar'" ref="empresaList" />
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import EmpresaForm from '../components/empresa/EmpresaForm.vue';
import EmpresaList from '../components/empresa/EmpresaList.vue';

const activeTab = ref<'cadastrar' | 'listar'>('cadastrar');
const empresaList = ref<InstanceType<typeof EmpresaList>>();

const handleEmpresaCriada = () => {
  activeTab.value = 'listar';
  empresaList.value?.carregar();
};
</script>

<style scoped>
.empresas-view {
  padding: 2rem;
  max-width: 1200px;
  margin: 0 auto;
}

h1 {
  color: #2c3e50;
  margin-bottom: 2rem;
}

.tabs {
  display: flex;
  gap: 1rem;
  margin-bottom: 2rem;
  border-bottom: 2px solid #ecf0f1;
}

.tabs button {
  padding: 0.75rem 1.5rem;
  background: none;
  border: none;
  color: #7f8c8d;
  font-size: 1rem;
  font-weight: 600;
  cursor: pointer;
  border-bottom: 3px solid transparent;
  transition: all 0.3s;
}

.tabs button:hover {
  color: #3498db;
}

.tabs button.active {
  color: #3498db;
  border-bottom-color: #3498db;
}

.content {
  min-height: 400px;
}
</style>
