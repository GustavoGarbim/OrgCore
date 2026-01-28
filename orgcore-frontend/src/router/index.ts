import { createRouter, createWebHistory } from 'vue-router';
import Home from '../views/Home.vue';
import EmpresasView from '../views/EmpresasView.vue';
import PessoasView from '../views/PessoasView.vue';
import ColaboradoresView from '../views/ColaboradoresView.vue';

const router = createRouter({
    history: createWebHistory(),
    routes: [
        {
            path: '/',
            name: 'home',
            component: Home
        },
        {
            path: '/empresas',
            name: 'empresas',
            component: EmpresasView
        },
        {
            path: '/pessoas',
            name: 'pessoas',
            component: PessoasView
        },
        {
            path: '/colaboradores',
            name: 'colaboradores',
            component: ColaboradoresView
        }
    ]
});

export default router;
