<template>
  <div class="container">
    <div class="page-header">
      <h1>Formulários</h1>
      <router-link v-if="authStore.isGestor" to="/formularios/criar" class="btn btn-primary">
        + Criar Formulário
      </router-link>
    </div>

    <div class="filter-section">
      <div class="form-group">
        <label for="empresaSelect">Filtrar por Empresa</label>
        <select id="empresaSelect" v-model="empresaSelecionada" @change="carregarFormularios">
          <option :value="null">Selecione uma empresa</option>
          <option v-for="empresa in empresas" :key="empresa.id" :value="empresa.id">
            {{ empresa.nomeFantasia }}
          </option>
        </select>
      </div>
    </div>

    <LoadingSpinner v-if="loading" message="Carregando formulários..." />

    <div v-else-if="!empresaSelecionada" class="empty-state">
      <p>Selecione uma empresa para ver os formulários</p>
    </div>

    <div v-else-if="formularios.length === 0" class="empty-state">
      <p>Nenhum formulário criado para esta empresa ainda.</p>
      <router-link v-if="authStore.isGestor" to="/formularios/criar" class="btn btn-primary">
        Criar Primeiro Formulário
      </router-link>
    </div>

    <div v-else class="formularios-grid">
      <div v-for="formulario in formularios" :key="formulario.id" class="formulario-card">
        <h3>{{ formulario.titulo }}</h3>
        <p class="descricao">{{ formulario.descricao }}</p>
        <div class="formulario-meta">
          <span class="badge">{{ formulario.secoes?.length || 0 }} seções</span>
        </div>
        <div class="formulario-actions">
          <router-link :to="`/formularios/${formulario.id}/responder`" class="btn btn-primary-small">
            Responder
          </router-link>
          <router-link v-if="authStore.isGestor" :to="`/avaliacoes?formularioId=${formulario.id}`" class="btn btn-secondary-small">
            Ver Respostas
          </router-link>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted, computed } from 'vue'
import { useAuthStore } from '../../stores/auth'
import { useEmpresaStore } from '../../stores/empresa'
import { useFormularioStore } from '../../stores/formulario'
import LoadingSpinner from '../../components/common/LoadingSpinner.vue'

const authStore = useAuthStore()
const empresaStore = useEmpresaStore()
const formularioStore = useFormularioStore()

const empresaSelecionada = ref(null)
const loading = ref(false)

const empresas = computed(() => empresaStore.empresas)
const formularios = computed(() => formularioStore.formularios)

onMounted(async () => {
  await empresaStore.listarEmpresas()
})

const carregarFormularios = async () => {
  if (!empresaSelecionada.value) return
  
  loading.value = true
  await formularioStore.listarFormulariosPorEmpresa(empresaSelecionada.value)
  loading.value = false
}
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

.filter-section {
  background: white;
  border-radius: 12px;
  padding: 1.5rem;
  box-shadow: 0 1px 3px rgba(0, 0, 0, 0.1);
  margin-bottom: 2rem;
}

.form-group {
  margin-bottom: 0;
}

.form-group label {
  display: block;
  margin-bottom: 0.5rem;
  font-weight: 600;
  color: #374151;
}

.form-group select {
  width: 100%;
  padding: 0.75rem 1rem;
  border: 2px solid #e5e7eb;
  border-radius: 8px;
  font-size: 1rem;
  cursor: pointer;
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

.btn-primary-small {
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
  color: white;
  padding: 0.5rem 1rem;
  font-size: 0.875rem;
}

.btn-secondary-small {
  background: #f3f4f6;
  color: #374151;
  padding: 0.5rem 1rem;
  font-size: 0.875rem;
}

.btn-secondary-small:hover {
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

.formularios-grid {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(350px, 1fr));
  gap: 1.5rem;
}

.formulario-card {
  background: white;
  border-radius: 12px;
  padding: 1.5rem;
  box-shadow: 0 1px 3px rgba(0, 0, 0, 0.1);
  transition: transform 0.2s, box-shadow 0.2s;
}

.formulario-card:hover {
  transform: translateY(-4px);
  box-shadow: 0 10px 20px rgba(0, 0, 0, 0.1);
}

.formulario-card h3 {
  margin: 0 0 0.5rem 0;
  color: #111827;
  font-size: 1.25rem;
}

.descricao {
  color: #6b7280;
  font-size: 0.875rem;
  margin: 0 0 1rem 0;
}

.formulario-meta {
  margin-bottom: 1rem;
}

.badge {
  background: #f3f4f6;
  color: #374151;
  padding: 0.25rem 0.75rem;
  border-radius: 12px;
  font-size: 0.75rem;
}

.formulario-actions {
  display: flex;
  gap: 0.5rem;
}
</style>
