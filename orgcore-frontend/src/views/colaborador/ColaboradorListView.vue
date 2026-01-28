<template>
  <div class="container">
    <div class="page-header">
      <h1>Colaboradores</h1>
      <router-link to="/colaboradores/vincular" class="btn btn-primary">+ Vincular Colaborador</router-link>
    </div>

    <div class="filter-section">
      <div class="form-group">
        <label for="empresaSelect">Filtrar por Empresa</label>
        <select id="empresaSelect" v-model="empresaSelecionada" @change="carregarColaboradores">
          <option :value="null">Selecione uma empresa</option>
          <option v-for="empresa in empresas" :key="empresa.id" :value="empresa.id">
            {{ empresa.nomeFantasia }}
          </option>
        </select>
      </div>
    </div>

    <LoadingSpinner v-if="loading" message="Carregando colaboradores..." />

    <div v-else-if="!empresaSelecionada" class="empty-state">
      <p>Selecione uma empresa para ver os colaboradores</p>
    </div>

    <div v-else-if="colaboradores.length === 0" class="empty-state">
      <p>Nenhum colaborador vinculado a esta empresa ainda.</p>
      <router-link to="/colaboradores/vincular" class="btn btn-primary">Vincular Primeiro Colaborador</router-link>
    </div>

    <div v-else class="colaboradores-list">
      <div v-for="colaborador in colaboradores" :key="colaborador.colaboradorId" class="colaborador-card">
        <div class="colaborador-info">
          <h3>{{ colaborador.nomePessoa }}</h3>
          <p class="empresa-nome">{{ colaborador.nomeEmpresa }}</p>
          <div class="colaborador-meta">
            <span class="badge">Colaborador ID: {{ colaborador.colaboradorId.substring(0, 8) }}...</span>
          </div>
        </div>
        <div class="colaborador-actions">
          <button class="btn btn-secondary" @click="atribuirCargo(colaborador)">
            Atribuir Cargo
          </button>
        </div>
      </div>
    </div>

    <!-- Modal para atribuir cargo -->
    <Modal :show="showCargoModal" title="Atribuir Cargo" @close="showCargoModal = false">
      <div class="modal-content-cargo">
        <p><strong>Colaborador:</strong> {{ colaboradorSelecionado?.nomePessoa }}</p>
        
        <div class="form-group">
          <label for="cargoSelect">Selecione o Cargo</label>
          <select id="cargoSelect" v-model="cargoSelecionado">
            <option :value="null">Selecione um cargo</option>
            <option v-for="(label, value) in CargosLabels" :key="value" :value="parseInt(value)">
              {{ label }}
            </option>
          </select>
        </div>

        <div v-if="errorMessage" class="alert alert-error">
          {{ errorMessage }}
        </div>

        <div v-if="successMessage" class="alert alert-success">
          {{ successMessage }}
        </div>
      </div>

      <template #footer>
        <button class="btn btn-secondary" @click="showCargoModal = false">Cancelar</button>
        <button class="btn btn-primary" @click="salvarCargo" :disabled="!cargoSelecionado || loadingCargo">
          {{ loadingCargo ? 'Salvando...' : 'Salvar' }}
        </button>
      </template>
    </Modal>
  </div>
</template>

<script setup>
import { ref, onMounted, computed } from 'vue'
import { useEmpresaStore } from '../../stores/empresa'
import { useColaboradorStore } from '../../stores/colaborador'
import { CargosLabels } from '../../constants/enums'
import LoadingSpinner from '../../components/common/LoadingSpinner.vue'
import Modal from '../../components/common/Modal.vue'

const empresaStore = useEmpresaStore()
const colaboradorStore = useColaboradorStore()

const empresaSelecionada = ref(null)
const loading = ref(false)
const loadingCargo = ref(false)
const showCargoModal = ref(false)
const colaboradorSelecionado = ref(null)
const cargoSelecionado = ref(null)
const errorMessage = ref('')
const successMessage = ref('')

const empresas = computed(() => empresaStore.empresas)
const colaboradores = computed(() => colaboradorStore.colaboradores)

onMounted(async () => {
  await empresaStore.listarEmpresas()
})

const carregarColaboradores = async () => {
  if (!empresaSelecionada.value) return
  
  loading.value = true
  await colaboradorStore.listarColaboradoresPorEmpresa(empresaSelecionada.value)
  loading.value = false
}

const atribuirCargo = (colaborador) => {
  colaboradorSelecionado.value = colaborador
  cargoSelecionado.value = null
  errorMessage.value = ''
  successMessage.value = ''
  showCargoModal.value = true
}

const salvarCargo = async () => {
  loadingCargo.value = true
  errorMessage.value = ''
  successMessage.value = ''

  const result = await empresaStore.atribuirCargo(
    empresaSelecionada.value,
    colaboradorSelecionado.value.colaboradorId,
    cargoSelecionado.value
  )

  if (result.success) {
    successMessage.value = 'Cargo atribuído com sucesso!'
    setTimeout(() => {
      showCargoModal.value = false
    }, 1500)
  } else {
    errorMessage.value = result.error || 'Erro ao atribuir cargo'
  }

  loadingCargo.value = false
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

.colaboradores-list {
  display: flex;
  flex-direction: column;
  gap: 1rem;
}

.colaborador-card {
  background: white;
  border-radius: 12px;
  padding: 1.5rem;
  box-shadow: 0 1px 3px rgba(0, 0, 0, 0.1);
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.colaborador-info h3 {
  margin: 0 0 0.25rem 0;
  color: #111827;
}

.empresa-nome {
  margin: 0 0 0.5rem 0;
  color: #6b7280;
  font-size: 0.875rem;
}

.colaborador-meta {
  display: flex;
  gap: 0.5rem;
}

.badge {
  background: #f3f4f6;
  color: #374151;
  padding: 0.25rem 0.75rem;
  border-radius: 12px;
  font-size: 0.75rem;
}

.modal-content-cargo {
  padding: 1rem 0;
}

.modal-content-cargo p {
  margin-bottom: 1.5rem;
  color: #374151;
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

.alert-success {
  background-color: #d1fae5;
  color: #065f46;
  border: 1px solid #6ee7b7;
}
</style>
