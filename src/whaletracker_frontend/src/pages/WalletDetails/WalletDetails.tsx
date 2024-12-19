import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { Grid, Typography, Paper } from '@mui/material';
import { Wallet } from '../../types/models';
import { walletService } from '../../services/walletService';
import { signalRService } from '../../services/signalRService';
import TokenList from '../../components/TokenList/TokenList';
import { ManualUpdate } from './ManualUpdate';

export const WalletDetails: React.FC = () => {
    const { id } = useParams<{ id: string }>();
    const [wallet, setWallet] = useState<Wallet | null>(null);

    useEffect(() => {
        if (id) {
            loadWallet();
            //signalRService.subscribeToWallet(id, handleTokenAdded);
        }

        return () => {
            if (id) {
                //signalRService.unsubscribeFromWallet(id);
            }
        };
    }, [id]);

    const loadWallet = async () => {
        if (id) {
            try {
                const data = await walletService.getWalletById(id);
                setWallet(data);
            } catch (error) {
                console.error('Error loading wallet:', error);
            }
        }
    };

    const handleTokenAdded = () => {
        loadWallet();
    };

    if (!wallet) {
        return <Typography>Loading...</Typography>;
    }

    return (
        <div>
            <Typography variant="h4" gutterBottom>
                Wallet Details
            </Typography>
            <Paper sx={{ p: 3, mb: 3 }}>
                <Typography variant="h6">{wallet.name || 'Unnamed Wallet'}</Typography>
                <Typography color="textSecondary">{wallet.address}</Typography>
            </Paper>

            <ManualUpdate 
                walletAddress={wallet.address} 
                onUpdateComplete={loadWallet} 
            />

            <TokenList tokens={wallet.tokens} />
        </div>
    );
}; 