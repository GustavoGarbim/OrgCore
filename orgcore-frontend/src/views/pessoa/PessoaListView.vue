<template>
  <div class="container">
    <div class="page-header">
      <h1>Pessoas Registradas</h1>
      <router-link to="/pessoas/registrar" class="btn btn-primary">+ Registrar Pessoa</router-link>
    </div>

    <div class="info-box">
      <p>Pessoas registradas podem ser vinculadas como colaboradores às empresas.</p>
    </div>

    <div class="actions-footer">
      <router-link to="/colaboradores/vincular" class="btn btn-secondary">
        Vincular Pessoa a Empresa
      </router-link>
    </div>
  </div>
</template>

<script setup>
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
}

.btn-secondary:hover {
  background: #e5e7eb;
}

.info-box {
  background: white;
  border-radius: 12px;
  padding: 2rem;
  box-shadow: 0 1px 3px rgba(0, 0, 0, 0.1);
  margin-bottom: 2rem;
}

.info-box p {
  margin: 0;
  color: #6b7280;
}

.actions-footer {
  margin-top: 2rem;
}
</style>
