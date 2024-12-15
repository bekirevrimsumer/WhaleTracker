import React, { useEffect, useState } from 'react';
import { Grid, Typography, Button } from '@mui/material';
import { Link } from 'react-router-dom';
import { Wallet } from '../../types/models';
import { walletService } from '../../services/walletService';
import WalletCard from '../../components/WalletCard/WalletCard';

export const Dashboard: React.FC = () => {
    const [wallets, setWallets] = useState<Wallet[]>([]);

    useEffect(() => {
        loadWallets();
    }, []);

    const loadWallets = async () => {
        try {
            const data = await walletService.getAllWallets();
            setWallets(data);
        } catch (error) {
            console.error('Error loading wallets:', error);
        }
    };

    return (
        <div>
            <Grid container spacing={3} alignItems="center" marginBottom={3}>
                <Grid item xs>
                    <Typography variant="h4">Wallet Dashboard</Typography>
                </Grid>
                <Grid item>
                    <Button
                        component={Link}
                        to="/add-wallet"
                        variant="contained"
                        color="primary"
                    >
                        Add New Wallet
                    </Button>
                </Grid>
            </Grid>

            <Grid container spacing={3}>
                {wallets.map((wallet) => (
                    <Grid item xs={12} sm={6} md={6} key={wallet.id}>
                        <WalletCard wallet={wallet} onDelete={loadWallets} />
                    </Grid>
                ))}
            </Grid>
        </div>
    );
}; 