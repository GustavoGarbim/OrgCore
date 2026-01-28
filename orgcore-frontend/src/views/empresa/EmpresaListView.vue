<template>
  <div class="container">
    <div class="page-header">
      <h1>Empresas</h1>
      <router-link to="/empresas/criar" class="btn btn-primary">+ Nova Empresa</router-link>
    </div>

    <LoadingSpinner v-if="loading" message="Carregando empresas..." />

    <div v-else-if="empresas.length === 0" class="empty-state">
      <p>Nenhuma empresa cadastrada ainda.</p>
      <router-link to="/empresas/criar" class="btn btn-primary">Cadastrar Primeira Empresa</router-link>
    </div>

    <div v-else class="empresas-grid">
      <div v-for="empresa in empresas" :key="empresa.id" class="empresa-card">
        <h3>{{ empresa.nomeFantasia }}</h3>
        <p class="cnpj">CNPJ: {{ formatarCNPJ(empresa.cnpj) }}</p>
        <div class="empresa-info">
          <p><strong>Admin:</strong> {{ empresa.nomeAdmin }}</p>
          <p><strong>Email:</strong> {{ empresa.emailAdmin }}</p>
        </div>
        <div class="empresa-actions">
          <router-link :to="`/colaboradores?empresaId=${empresa.id}`" class="btn btn-secondary">
            Ver Colaboradores
          </router-link>
          <router-link :to="`/formularios?empresaId=${empresa.id}`" class="btn btn-secondary">
            Ver Formulários
          </router-link>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted, computed } from 'vue'
import { useEmpresaStore } from '../../stores/empresa'
import { formatarCNPJ } from '../../utils/formatters'
import LoadingSpinner from '../../components/common/LoadingSpinner.vue'

const empresaStore = useEmpresaStore()

const loading = ref(false)
const empresas = computed(() => empresaStore.empresas)

onMounted(async () => {
  loading.value = true
  await empresaStore.listarEmpresas()
  loading.value = false
})
</script>

<style scoped>
.container {
  max-width: 1200px;
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
  font-size: 2rem;
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

.btn-primary:hover {
  transform: translateY(-2px);
  box-shadow: 0 6px 20px rgba(102, 126, 234, 0.4);
}

.btn-secondary {
  background: #f3f4f6;
  color: #374151;
  font-size: 0.875rem;
  padding: 0.5rem 1rem;
}

.btn-secondary:hover {
  background: #e5e7eb;
}

.empty-state {
  text-align: center;
  padding: 4rem 2rem;
  background: white;
  border-radius: 12px;
}

.empty-state p {
  color: #6b7280;
  margin-bottom: 1.5rem;
  font-size: 1.125rem;
}

.empresas-grid {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(350px, 1fr));
  gap: 1.5rem;
}

.empresa-card {
  background: white;
  border-radius: 12px;
  padding: 1.5rem;
  box-shadow: 0 1px 3px rgba(0, 0, 0, 0.1);
  transition: transform 0.2s, box-shadow 0.2s;
}

.empresa-card:hover {
  transform: translateY(-4px);
  box-shadow: 0 10px 20px rgba(0, 0, 0, 0.1);
}

.empresa-card h3 {
  font-size: 1.25rem;
  color: #111827;
  margin: 0 0 0.5rem 0;
}

.cnpj {
  color: #6b7280;
  font-size: 0.875rem;
  margin: 0 0 1rem 0;
}

.empresa-info {
  margin-bottom: 1.5rem;
  padding: 1rem;
  background: #f9fafb;
  border-radius: 8px;
}

.empresa-info p {
  margin: 0.25rem 0;
  font-size: 0.875rem;
  color: #374151;
}

.empresa-actions {
  display: flex;
  gap: 0.5rem;
}
</style>
