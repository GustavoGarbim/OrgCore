<template>
  <div class="container">
    <div class="page-header">
      <h1>Detalhes da Avaliação</h1>
      <router-link to="/avaliacoes" class="btn btn-secondary">← Voltar</router-link>
    </div>

    <LoadingSpinner v-if="loading" message="Carregando avaliação..." />

    <div v-else-if="avaliacao" class="avaliacao-detail">
      <div class="avaliacao-header">
        <h2>{{ avaliacao.tituloFormulario }}</h2>
        <div class="meta-info">
          <p><strong>Colaborador:</strong> {{ avaliacao.nomeColaborador }}</p>
          <p><strong>Data:</strong> {{ formatarDataHora(avaliacao.dataResposta) }}</p>
        </div>
      </div>

      <div class="respostas-list">
        <div v-for="(resposta, index) in avaliacao.respostas" :key="index" class="resposta-card">
          <h3>{{ resposta.enunciadoQuestao }}</h3>
          <div class="resposta-valor">
            <span class="tipo-badge">{{ resposta.tipo }}</span>
            <p class="valor">{{ resposta.valorResposta }}</p>
          </div>
        </div>
      </div>
    </div>

    <div v-else class="empty-state">
      <p>Avaliação não encontrada</p>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { useRoute } from 'vue-router'
import { useFormularioStore } from '../../stores/formulario'
import { formatarDataHora } from '../../utils/formatters'
import LoadingSpinner from '../../components/common/LoadingSpinner.vue'

const route = useRoute()
const formularioStore = useFormularioStore()

const avaliacao = ref(null)
const loading = ref(false)

onMounted(async () => {
  loading.value = true
  const result = await formularioStore.obterRelatorio(route.params.id)
  if (result.success) {
    avaliacao.value = result.data
  }
  loading.value = false
})
</script>

<style scoped>
.container {
  max-width: 900px;
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
  font-size: 1.75rem;
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

.btn-secondary {
  background: #f3f4f6;
  color: #374151;
}

.btn-secondary:hover {
  background: #e5e7eb;
}

.avaliacao-detail {
  background: white;
  border-radius: 12px;
  padding: 2rem;
  box-shadow: 0 1px 3px rgba(0, 0, 0, 0.1);
}

.avaliacao-header {
  padding-bottom: 1.5rem;
  margin-bottom: 2rem;
  border-bottom: 2px solid #e5e7eb;
}

.avaliacao-header h2 {
  margin: 0 0 1rem 0;
  color: #111827;
}

.meta-info p {
  margin: 0.5rem 0;
  color: #6b7280;
}

.respostas-list {
  display: flex;
  flex-direction: column;
  gap: 1.5rem;
}

.resposta-card {
  background: #f9fafb;
  border-radius: 8px;
  padding: 1.5rem;
}

.resposta-card h3 {
  margin: 0 0 1rem 0;
  color: #111827;
  font-size: 1.125rem;
}

.resposta-valor {
  display: flex;
  align-items: center;
  gap: 1rem;
}

.tipo-badge {
  background: #dbeafe;
  color: #1e40af;
  padding: 0.25rem 0.75rem;
  border-radius: 12px;
  font-size: 0.75rem;
  font-weight: 600;
}

.valor {
  margin: 0;
  color: #374151;
  font-size: 1.125rem;
}

.empty-state {
  text-align: center;
  padding: 4rem 2rem;
  background: white;
  border-radius: 12px;
}

.empty-state p {
  color: #6b7280;
  font-size: 1.125rem;
}
</style>
