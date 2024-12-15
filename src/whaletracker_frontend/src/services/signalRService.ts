import * as signalR from '@microsoft/signalr';
import { Token } from '../types/models';

export class SignalRService {
    private hubConnection: signalR.HubConnection;

    constructor() {
        this.hubConnection = new signalR.HubConnectionBuilder()
            .withUrl('https://localhost:44338/hubs/wallet')
            .withAutomaticReconnect()
            .build();
    }

    public async startConnection(): Promise<void> {
        try {
            await this.hubConnection.start();
            console.log('SignalR Connected!');
        } catch (err) {
            console.error('Error while establishing connection: ', err);
        }
    }

    public subscribeToWallet(walletId: string, onTokenAdded: (token: Token) => void): void {
        this.hubConnection.invoke('JoinWalletGroup', walletId);
        
        this.hubConnection.on('TokenAdded', (notification) => {
            onTokenAdded(notification);
        });
    }

    public unsubscribeFromWallet(walletId: string): void {
        this.hubConnection.invoke('LeaveWalletGroup', walletId);
    }
}

export const signalRService = new SignalRService(); 