<template>
  <div class="home">
    <h1>OrgCore - Gerenciador de Empresas</h1>
    <p class="subtitle">Sistema de gestão de empresas, colaboradores e pessoas</p>
    
    <div class="cards">
      <router-link to="/empresas" class="card">
        <h2>🏢 Empresas</h2>
        <p>Cadastre e gerencie suas empresas</p>
      </router-link>
      
      <router-link to="/pessoas" class="card">
        <h2>👤 Pessoas</h2>
        <p>Gerencie pessoas do sistema</p>
      </router-link>
      
      <router-link to="/colaboradores" class="card">
        <h2>👥 Colaboradores</h2>
        <p>Vincule pessoas às empresas</p>
      </router-link>
    </div>
  </div>
</template>

<script setup lang="ts">
</script>

<style scoped>
.home {
  padding: 3rem;
  max-width: 1200px;
  margin: 0 auto;
}

h1 {
  font-size: 2.5rem;
  color: #2c3e50;
  margin-bottom: 0.5rem;
}

.subtitle {
  color: #7f8c8d;
  font-size: 1.125rem;
  margin-bottom: 3rem;
}

.cards {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(300px, 1fr));
  gap: 2rem;
}

.card {
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
  color: white;
  padding: 2rem;
  border-radius: 12px;
  text-decoration: none;
  transition: transform 0.3s, box-shadow 0.3s;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
}

.card:hover {
  transform: translateY(-8px);
  box-shadow: 0 12px 24px rgba(0, 0, 0, 0.2);
}

.card:nth-child(1) {
  background: linear-gradient(135deg, #3498db 0%, #2980b9 100%);
}

.card:nth-child(2) {
  background: linear-gradient(135deg, #27ae60 0%, #229954 100%);
}

.card:nth-child(3) {
  background: linear-gradient(135deg, #9b59b6 0%, #8e44ad 100%);
}

.card h2 {
  margin: 0 0 1rem 0;
  font-size: 1.75rem;
}

.card p {
  margin: 0;
  opacity: 0.9;
}
</style>
