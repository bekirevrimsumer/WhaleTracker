import React from 'react';
import { Card, CardContent, CardActions, Typography, Button, IconButton, Box } from '@mui/material';
import { Delete as DeleteIcon, Edit as EditIcon } from '@mui/icons-material';
import { useNavigate } from 'react-router-dom';
import { Wallet } from '../../types/models';
import { walletService } from '../../services/walletService';

interface WalletCardProps {
    wallet: Wallet;
    onDelete: () => void;
}

const WalletCard: React.FC<WalletCardProps> = ({ wallet, onDelete }) => {
    const navigate = useNavigate();

    const handleDelete = async () => {
        try {
            await walletService.deleteWallet(wallet.id);
            onDelete();
        } catch (error) {
            console.error('Error deleting wallet:', error);
        }
    };

    const totalValue = wallet.tokens?.reduce((sum, token) => 
        sum + (token.balance * token.price), 0);

    return (
        <Card>
            <CardContent>
                <Box display="flex" justifyContent="space-between" alignItems="center" mb={2}>
                    <Typography variant="h6" component="div">
                        {wallet.name || 'Unnamed Wallet'}
                    </Typography>
                    <Box>
                        <IconButton 
                            size="small" 
                            onClick={handleDelete}
                            color="error"
                        >
                            <DeleteIcon />
                        </IconButton>
                    </Box>
                </Box>
                
                <Typography color="textSecondary" gutterBottom>
                    {wallet.address}
                </Typography>
                
                <Typography variant="body2" color="textSecondary">
                    Total Value: ${totalValue?.toFixed(2)}
                </Typography>
                
                <Typography variant="body2" color="textSecondary">
                    Tokens: {wallet.tokens?.length}
                </Typography>
            </CardContent>
            
            <CardActions>
                <Button 
                    size="small" 
                    color="primary"
                    onClick={() => navigate(`/wallet/${wallet.id}`)}
                >
                    View Details
                </Button>
            </CardActions>
        </Card>
    );
};

export default WalletCard; 