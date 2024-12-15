import axios from 'axios';
import { Wallet, CreateWalletDto, UpdateWalletDto } from '../types/models';

const API_URL = 'https://localhost:44338/api';

export const walletService = {
    getAllWallets: async (): Promise<Wallet[]> => {
        const response = await axios.get(`${API_URL}/wallets`);
        return response.data;
    },

    getWalletById: async (id: string): Promise<Wallet> => {
        const response = await axios.get(`${API_URL}/wallets/${id}`);
        return response.data;
    },

    createWallet: async (wallet: CreateWalletDto): Promise<Wallet> => {
        const response = await axios.post(`${API_URL}/wallets`, wallet);
        return response.data;
    },

    updateWallet: async (id: string, wallet: UpdateWalletDto): Promise<void> => {
        await axios.put(`${API_URL}/wallets/${id}`, wallet);
    },

    deleteWallet: async (id: string): Promise<void> => {
        await axios.delete(`${API_URL}/wallets/${id}`);
    }
}; 