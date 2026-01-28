import { createRouter, createWebHistory } from 'vue-router'
import { useAuthStore } from '../stores/auth'

const routes = [
    {
        path: '/login',
        name: 'Login',
        component: () => import('../views/LoginView.vue'),
        meta: { requiresAuth: false }
    },
    {
        path: '/',
        redirect: '/dashboard'
    },
    {
        path: '/dashboard',
        name: 'Dashboard',
        component: () => import('../views/DashboardView.vue'),
        meta: { requiresAuth: true }
    },
    {
        path: '/empresas',
        name: 'Empresas',
        component: () => import('../views/empresa/EmpresaListView.vue'),
        meta: { requiresAuth: true, requiresGestor: true }
    },
    {
        path: '/empresas/criar',
        name: 'CriarEmpresa',
        component: () => import('../views/empresa/EmpresaCreateView.vue'),
        meta: { requiresAuth: true, requiresGestor: true }
    },
    {
        path: '/pessoas',
        name: 'Pessoas',
        component: () => import('../views/pessoa/PessoaListView.vue'),
        meta: { requiresAuth: true, requiresGestor: true }
    },
    {
        path: '/pessoas/registrar',
        name: 'RegistrarPessoa',
        component: () => import('../views/pessoa/PessoaCreateView.vue'),
        meta: { requiresAuth: true, requiresGestor: true }
    },
    {
        path: '/colaboradores',
        name: 'Colaboradores',
        component: () => import('../views/colaborador/ColaboradorListView.vue'),
        meta: { requiresAuth: true, requiresGestor: true }
    },
    {
        path: '/colaboradores/vincular',
        name: 'VincularColaborador',
        component: () => import('../views/colaborador/ColaboradorVincularView.vue'),
        meta: { requiresAuth: true, requiresGestor: true }
    },
    {
        path: '/formularios',
        name: 'Formularios',
        component: () => import('../views/formulario/FormularioListView.vue'),
        meta: { requiresAuth: true }
    },
    {
        path: '/formularios/criar',
        name: 'CriarFormulario',
        component: () => import('../views/formulario/FormularioCreateView.vue'),
        meta: { requiresAuth: true, requiresGestor: true }
    },
    {
        path: '/formularios/:id/responder',
        name: 'ResponderFormulario',
        component: () => import('../views/formulario/FormularioResponderView.vue'),
        meta: { requiresAuth: true }
    },
    {
        path: '/avaliacoes',
        name: 'Avaliacoes',
        component: () => import('../views/avaliacao/AvaliacaoListView.vue'),
        meta: { requiresAuth: true, requiresGestor: true }
    },
    {
        path: '/avaliacoes/:id',
        name: 'AvaliacaoDetalhes',
        component: () => import('../views/avaliacao/AvaliacaoDetailView.vue'),
        meta: { requiresAuth: true, requiresGestor: true }
    }
]

const router = createRouter({
    history: createWebHistory(),
    routes
})

// Navigation guard
router.beforeEach((to, from, next) => {
    const authStore = useAuthStore()
    const requiresAuth = to.matched.some(record => record.meta.requiresAuth)
    const requiresGestor = to.matched.some(record => record.meta.requiresGestor)

    if (requiresAuth && !authStore.isAuthenticated) {
        next('/login')
    } else if (requiresGestor && !authStore.isGestor) {
        // Usuário não tem permissão de gestor
        next('/dashboard')
    } else {
        next()
    }
})

export default router
