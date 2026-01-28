import axios from 'axios';

const api = axios.create({
    baseURL: '/api',
    headers: {
        'Content-Type': 'application/json',
    },
});

// Interceptor para tratamento de erros global
api.interceptors.response.use(
    (response) => response,
    (error) => {
        console.error('Erro na requisição:', error);
        return Promise.reject(error);
    }
);

export default api;
