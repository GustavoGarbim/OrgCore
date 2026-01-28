
import api from './api';
import type { CadastrarColaboradorDTO, Colaborador } from '../types/Colaborador';

export const colaboradorService = {
    async vincular(dto: CadastrarColaboradorDTO) {
        const response = await api.post<string>('/Colaborador', dto);
        return response.data;
    },

    async obterPorEmpresa(empresaId: string) {
        const response = await api.get<Colaborador[]>(`/Colaborador/${empresaId}`);
        return response.data;
    },

    async obterPorId(colaboradorId: string) {
        const response = await api.get<Colaborador>(`/Colaborador/colaborador/${colaboradorId}`);
        return response.data;
    }
};
