# OrgCore Frontend

Frontend em Vue 3 + TypeScript para o sistema OrgCore (Gerenciador de Empresas).

## 🚀 Tecnologias

- **Vue 3** (Composition API)
- **TypeScript**
- **Axios** (Comunicação HTTP)
- **Vue Router** (Roteamento)
- **Vite** (Build tool)

## 📦 Instalação

```bash
npm install
```

## 🛠️ Desenvolvimento

```bash
npm run dev
```

O servidor será iniciado em `http://localhost:5173`

## 🏗️ Build para Produção

```bash
npm run build
```

## ⚙️ Configuração da API

Por padrão, o projeto está configurado para fazer proxy das requisições `/api` para `http://localhost:5000`.

Para alterar isso, edite o arquivo `vite.config.ts`:

```typescript
server: {
  proxy: {
    '/api': {
      target: 'http://localhost:PORTA_DA_SUA_API',
      changeOrigin: true,
    }
  }
}
```

## 📂 Estrutura do Projeto

```
src/
├── components/      # Componentes reutilizáveis
│   ├── empresa/
│   ├── pessoa/
│   └── colaborador/
├── services/        # Serviços HTTP (Axios)
├── types/           # Interfaces TypeScript
├── views/           # Páginas/Views
├── router/          # Configuração de rotas
├── App.vue          # Componente raiz
└── main.ts          # Entrada da aplicação
```

## 🎯 Funcionalidades

- ✅ Cadastro de Empresas
- ✅ Listagem de Empresas
- ✅ Cadastro de Pessoas
- ✅ Vinculação de Colaboradores
