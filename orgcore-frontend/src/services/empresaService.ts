import api from './api';
import type { EmpresaDTO, Empresa, AtribuirCargoDTO } from '../types/Empresa';

export const empresaService = {
    async criar(dto: EmpresaDTO) {
        const response = await api.post<{ id: string; mensagem: string }>('/Empresa', dto);
        return response.data;
    },

    async obterTodas() {
        const response = await api.get<Empresa[]>('/Empresa');
        return response.data;
    },

    async atribuirCargo(dto: AtribuirCargoDTO) {
        const response = await api.post(`/Empresa/${dto.colaboradorId}`, dto);
        return response.data;
    }
};
