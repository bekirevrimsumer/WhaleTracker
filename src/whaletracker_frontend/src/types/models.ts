export interface Wallet {
    id: string;
    address: string;
    name?: string;
    isActive: boolean;
    createdAt: string;
    tokens: Token[];
}

export interface Token {
    tokenAddress: string;
    tokenName: string;
    tokenSymbol: string;
    balance: number;
    price: number;
    tokenIcon?: string;
}

export interface CreateWalletDto {
    address: string;
    name?: string;
    isActive: boolean;
}

export interface UpdateWalletDto {
    name?: string;
    isActive: boolean;
} 