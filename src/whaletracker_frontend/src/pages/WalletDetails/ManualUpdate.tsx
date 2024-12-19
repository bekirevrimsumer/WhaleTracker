import React, { useState } from 'react';
import { 
    Paper, 
    Typography, 
    TextField, 
    Button, 
    Box,
    Alert
} from '@mui/material';
import { walletService } from '../../services/walletService';

interface ManualUpdateProps {
    walletAddress: string;
    onUpdateComplete: () => void;
}

export const ManualUpdate: React.FC<ManualUpdateProps> = ({ walletAddress, onUpdateComplete }) => {
    const [tokenData, setTokenData] = useState('');
    const [error, setError] = useState('');
    const [success, setSuccess] = useState(false);

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        setError('');
        setSuccess(false);

        try {
            // JSON formatını kontrol et
            JSON.parse(tokenData);

            await walletService.updateTokensManually(walletAddress, tokenData);
            setSuccess(true);
            onUpdateComplete();
            setTokenData('');
        } catch (err: any) {
            if (err instanceof SyntaxError) {
                setError('Invalid JSON format');
            } else {
                setError(err.response?.data || 'Error updating tokens');
            }
        }
    };

    return (
        <Paper sx={{ p: 3, mb: 3 }}>
            <Typography variant="h6" gutterBottom>
                Manual Token Update
            </Typography>

            <form onSubmit={handleSubmit}>
                <TextField
                    fullWidth
                    multiline
                    rows={10}
                    label="Token Data (JSON)"
                    value={tokenData}
                    onChange={(e) => setTokenData(e.target.value)}
                    margin="normal"
                    required
                    error={!!error}
                    helperText={error}
                />

                {success && (
                    <Alert severity="success" sx={{ mt: 2, mb: 2 }}>
                        Tokens updated successfully!
                    </Alert>
                )}

                <Box sx={{ mt: 2 }}>
                    <Button
                        variant="contained"
                        color="primary"
                        type="submit"
                    >
                        Update Tokens
                    </Button>
                </Box>
            </form>
        </Paper>
    );
}; 