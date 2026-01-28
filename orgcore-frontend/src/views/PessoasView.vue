<template>
  <div class="pessoas-view">
    <h1>Gestão de Pessoas</h1>
    <PessoaForm />
  </div>
</template>

<script setup lang="ts">
import PessoaForm from '../components/pessoa/PessoaForm.vue';
</script>

<style scoped>
.pessoas-view {
  padding: 2rem;
  max-width: 1200px;
  margin: 0 auto;
}

h1 {
  color: #2c3e50;
  margin-bottom: 2rem;
}
</style>
