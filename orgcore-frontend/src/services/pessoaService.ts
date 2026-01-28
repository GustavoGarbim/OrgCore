import api from './api';
import type { CriarPessoaDto, Pessoa } from '../types/Pessoa';

export const pessoaService = {
    async registrar(dto: CriarPessoaDto) {
        const response = await api.post<{ id: string; nome: string; email: string; mensagem: string }>('/Pessoa', dto);
        return response.data;
    },

    async buscarPorId(id: string) {
        const response = await api.get<Pessoa>('/Pessoa', { params: { id } });
        return response.data;
    }
};
