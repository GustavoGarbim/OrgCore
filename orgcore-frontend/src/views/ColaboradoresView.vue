<template>
  <div class="colaboradores-view">
    <h1>Gestão de Colaboradores</h1>
    <ColaboradorForm />
  </div>
</template>

<script setup lang="ts">
import ColaboradorForm from '../components/colaborador/ColaboradorForm.vue';
</script>

<style scoped>
.colaboradores-view {
  padding: 2rem;
  max-width: 1200px;
  margin: 0 auto;
}

h1 {
  color: #2c3e50;
  margin-bottom: 2rem;
}
</style>
