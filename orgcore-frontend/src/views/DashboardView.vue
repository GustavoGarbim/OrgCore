<template>
  <div class="container">
    <div class="dashboard-header">
      <h1>Dashboard</h1>
      <p>Bem-vindo, {{ authStore.getUserName }}</p>
    </div>

    <div class="stats-grid">
      <div class="stat-card">
        <div class="stat-icon" style="background-color: #dbeafe;">
          <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" style="color: #3b82f6;">
            <path stroke-linecap="round" stroke-linejoin="round" d="M2.25 21h19.5m-18-18v18m10.5-18v18m6-13.5V21M6.75 6.75h.75m-.75 3h.75m-.75 3h.75m3-6h.75m-.75 3h.75m-.75 3h.75M6.75 21v-3.375c0-.621.504-1.125 1.125-1.125h2.25c.621 0 1.125.504 1.125 1.125V21M3 3h12m-.75 4.5H21m-3.75 3.75h.008v.008h-.008v-.008zm0 3h.008v.008h-.008v-.008zm0 3h.008v.008h-.008v-.008z" />
          </svg>
        </div>
        <div class="stat-info">
          <h3>{{ empresaStore.empresas.length }}</h3>
          <p>Empresas</p>
        </div>
      </div>

      <div class="stat-card">
        <div class="stat-icon" style="background-color: #dcfce7;">
          <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" style="color: #22c55e;">
            <path stroke-linecap="round" stroke-linejoin="round" d="M15 19.128a9.38 9.38 0 002.625.372 9.337 9.337 0 004.121-.952 4.125 4.125 0 00-7.533-2.493M15 19.128v-.003c0-1.113-.285-2.16-.786-3.07M15 19.128v.106A12.318 12.318 0 018.624 21c-2.331 0-4.512-.645-6.374-1.766l-.001-.109a6.375 6.375 0 0111.964-3.07M12 6.375a3.375 3.375 0 11-6.75 0 3.375 3.375 0 016.75 0zm8.25 2.25a2.625 2.625 0 11-5.25 0 2.625 2.625 0 015.25 0z" />
          </svg>
        </div>
        <div class="stat-info">
          <h3>{{ colaboradorStore.colaboradores.length }}</h3>
          <p>Colaboradores</p>
        </div>
      </div>

      <div class="stat-card">
        <div class="stat-icon" style="background-color: #fef3c7;">
          <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" style="color: #f59e0b;">
            <path stroke-linecap="round" stroke-linejoin="round" d="M9 12h3.75M9 15h3.75M9 18h3.75m3 .75H18a2.25 2.25 0 002.25-2.25V6.108c0-1.135-.845-2.098-1.976-2.192a48.424 48.424 0 00-1.123-.08m-5.801 0c-.065.21-.1.433-.1.664 0 .414.336.75.75.75h4.5a.75.75 0 00.75-.75 2.25 2.25 0 00-.1-.664m-5.8 0A2.251 2.251 0 0113.5 2.25H15c1.012 0 1.867.668 2.15 1.586m-5.8 0c-.376.023-.75.05-1.124.08C9.095 4.01 8.25 4.973 8.25 6.108V8.25m0 0H4.875c-.621 0-1.125.504-1.125 1.125v11.25c0 .621.504 1.125 1.125 1.125h9.75c.621 0 1.125-.504 1.125-1.125V9.375c0-.621-.504-1.125-1.125-1.125H8.25zM6.75 12h.008v.008H6.75V12zm0 3h.008v.008H6.75V15zm0 3h.008v.008H6.75V18z" />
          </svg>
        </div>
        <div class="stat-info">
          <h3>{{ formularioStore.formularios.length }}</h3>
          <p>Formulários</p>
        </div>
      </div>

      <div class="stat-card">
        <div class="stat-icon" style="background-color: #f3e8ff;">
          <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" style="color: #a855f7;">
            <path stroke-linecap="round" stroke-linejoin="round" d="M11.35 3.836c-.065.21-.1.433-.1.664 0 .414.336.75.75.75h4.5a.75.75 0 00.75-.75 2.25 2.25 0 00-.1-.664m-5.8 0A2.251 2.251 0 0113.5 2.25H15c1.012 0 1.867.668 2.15 1.586m-5.8 0c-.376.023-.75.05-1.124.08C9.095 4.01 8.25 4.973 8.25 6.108V8.25m8.9-4.414c.376.023.75.05 1.124.08 1.131.094 1.976 1.057 1.976 2.192V16.5A2.25 2.25 0 0118 18.75h-2.25m-7.5-10.5H4.875c-.621 0-1.125.504-1.125 1.125v11.25c0 .621.504 1.125 1.125 1.125h9.75c.621 0 1.125-.504 1.125-1.125V18.75m-7.5-10.5h6.375c.621 0 1.125.504 1.125 1.125v9.375m-8.25-3l1.5 1.5 3-3.75" />
          </svg>
        </div>
        <div class="stat-info">
          <h3>{{ avaliacoesCount }}</h3>
          <p>Avaliações</p>
        </div>
      </div>
    </div>

    <div class="quick-actions">
      <h2>Ações Rápidas</h2>
      <div class="actions-grid">
        <router-link v-if="authStore.isGestor" to="/empresas/criar" class="action-card">
          <div class="action-icon">+</div>
          <h3>Criar Empresa</h3>
          <p>Registrar nova empresa no sistema</p>
        </router-link>

        <router-link v-if="authStore.isGestor" to="/pessoas/registrar" class="action-card">
          <div class="action-icon">👤</div>
          <h3>Registrar Pessoa</h3>
          <p>Adicionar nova pessoa ao sistema</p>
        </router-link>

        <router-link v-if="authStore.isGestor" to="/formularios/criar" class="action-card">
          <div class="action-icon">📋</div>
          <h3>Criar Formulário</h3>
          <p>Criar novo formulário de avaliação</p>
        </router-link>

        <router-link to="/formularios" class="action-card">
          <div class="action-icon">✍️</div>
          <h3>Responder Formulários</h3>
          <p>Ver e responder formulários disponíveis</p>
        </router-link>
      </div>
    </div>

    <LoadingSpinner v-if="loading" message="Carregando dados..." />
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { useAuthStore } from '../stores/auth'
import { useEmpresaStore } from '../stores/empresa'
import { useColaboradorStore } from '../stores/colaborador'
import { useFormularioStore } from '../stores/formulario'
import LoadingSpinner from '../components/common/LoadingSpinner.vue'

const authStore = useAuthStore()
const empresaStore = useEmpresaStore()
const colaboradorStore = useColaboradorStore()
const formularioStore = useFormularioStore()

const loading = ref(false)
const avaliacoesCount = ref(0)

onMounted(async () => {
  loading.value = true
  
  if (authStore.isGestor) {
    await empresaStore.listarEmpresas()
  }
  
  // TODO: Carregar dados do colaborador conectado e suas métricas
  
  loading.value = false
})
</script>

<style scoped>
.container {
  max-width: 1200px;
  margin: 0 auto;
  padding: 0 2rem;
}

.dashboard-header {
  margin-bottom: 2rem;
}

.dashboard-header h1 {
  font-size: 2rem;
  color: #111827;
  margin-bottom: 0.5rem;
}

.dashboard-header p {
  color: #6b7280;
  font-size: 1.125rem;
}

.stats-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
  gap: 1.5rem;
  margin-bottom: 3rem;
}

.stat-card {
  background: white;
  border-radius: 12px;
  padding: 1.5rem;
  display: flex;
  align-items: center;
  gap: 1rem;
  box-shadow: 0 1px 3px rgba(0, 0, 0, 0.1);
  transition: transform 0.2s, box-shadow 0.2s;
}

.stat-card:hover {
  transform: translateY(-4px);
  box-shadow: 0 10px 20px rgba(0, 0, 0, 0.1);
}

.stat-icon {
  width: 60px;
  height: 60px;
  border-radius: 12px;
  display: flex;
  align-items: center;
  justify-content: center;
}

.stat-icon svg {
  width: 32px;
  height: 32px;
}

.stat-info h3 {
  font-size: 2rem;
  font-weight: 700;
  color: #111827;
  margin: 0;
}

.stat-info p {
  color: #6b7280;
  margin: 0;
  font-size: 0.875rem;
}

.quick-actions {
  margin-top: 3rem;
}

.quick-actions h2 {
  font-size: 1.5rem;
  color: #111827;
  margin-bottom: 1.5rem;
}

.actions-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
  gap: 1.5rem;
}

.action-card {
  background: white;
  border-radius: 12px;
  padding: 2rem;
  text-align: center;
  text-decoration: none;
  color: inherit;
  box-shadow: 0 1px 3px rgba(0, 0, 0, 0.1);
  transition: transform 0.2s, box-shadow 0.2s;
  cursor: pointer;
}

.action-card:hover {
  transform: translateY(-4px);
  box-shadow: 0 10px 20px rgba(0, 0, 0, 0.1);
}

.action-icon {
  font-size: 3rem;
  margin-bottom: 1rem;
}

.action-card h3 {
  font-size: 1.125rem;
  color: #111827;
  margin-bottom: 0.5rem;
}

.action-card p {
  color: #6b7280;
  font-size: 0.875rem;
  margin: 0;
}
</style>
