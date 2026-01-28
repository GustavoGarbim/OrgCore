<template>
  <div id="app">
    <header>
      <nav>
        <router-link to="/" class="logo">OrgCore</router-link>
        <div class="nav-links">
          <router-link to="/empresas">Empresas</router-link>
          <router-link to="/pessoas">Pessoas</router-link>
          <router-link to="/colaboradores">Colaboradores</router-link>
        </div>
      </nav>
    </header>
    
    <main>
      <router-view />
    </main>
    
    <footer>
      <p>&copy; 2026 OrgCore - Sistema de Gestão de Empresas</p>
    </footer>
  </div>
</template>

<script setup lang="ts">
</script>

<style>
* {
  margin: 0;
  padding: 0;
  box-sizing: border-box;
}

body {
  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
  background: #f5f7fa;
  color: #2c3e50;
}

#app {
  min-height: 100vh;
  display: flex;
  flex-direction: column;
}

header {
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
  color: white;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
}

nav {
  max-width: 1200px;
  margin: 0 auto;
  padding: 1rem 2rem;
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.logo {
  font-size: 1.5rem;
  font-weight: 700;
  text-decoration: none;
  color: white;
  letter-spacing: 1px;
}

.nav-links {
  display: flex;
  gap: 2rem;
}

.nav-links a {
  color: white;
  text-decoration: none;
  font-weight: 500;
  transition: opacity 0.3s;
}

.nav-links a:hover,
.nav-links a.router-link-active {
  opacity: 0.8;
  text-decoration: underline;
}

main {
  flex: 1;
  padding: 2rem 0;
}

footer {
  background: #34495e;
  color: white;
  text-align: center;
  padding: 1.5rem;
  margin-top: 3rem;
}

footer p {
  margin: 0;
}
</style>
