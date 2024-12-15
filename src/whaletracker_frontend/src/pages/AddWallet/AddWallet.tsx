import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import {
    Paper,
    Typography,
    TextField,
    Button,
    Box,
    FormControlLabel,
    Switch
} from '@mui/material';
import { walletService } from '../../services/walletService';

export const AddWallet: React.FC = () => {
    const navigate = useNavigate();
    const [formData, setFormData] = useState({
        address: '',
        name: '',
        isActive: true
    });
    const [error, setError] = useState('');

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        setError('');

        try {
            await walletService.createWallet(formData);
            navigate('/');
        } catch (err: any) {
            setError(err.response?.data || 'Error creating wallet');
        }
    };

    return (
        <Paper sx={{ p: 3, maxWidth: 600, mx: 'auto' }}>
            <Typography variant="h5" gutterBottom>
                Add New Wallet
            </Typography>

            <form onSubmit={handleSubmit}>
                <TextField
                    fullWidth
                    label="Wallet Address"
                    value={formData.address}
                    onChange={(e) => setFormData({ ...formData, address: e.target.value })}
                    margin="normal"
                    required
                />

                <TextField
                    fullWidth
                    label="Wallet Name (Optional)"
                    value={formData.name}
                    onChange={(e) => setFormData({ ...formData, name: e.target.value })}
                    margin="normal"
                />

                <FormControlLabel
                    control={
                        <Switch
                            checked={formData.isActive}
                            onChange={(e) => setFormData({ ...formData, isActive: e.target.checked })}
                        />
                    }
                    label="Active"
                />

                {error && (
                    <Typography color="error" sx={{ mt: 2 }}>
                        {error}
                    </Typography>
                )}

                <Box sx={{ mt: 3, display: 'flex', gap: 2 }}>
                    <Button
                        variant="contained"
                        color="primary"
                        type="submit"
                    >
                        Add Wallet
                    </Button>
                    <Button
                        variant="outlined"
                        onClick={() => navigate('/')}
                    >
                        Cancel
                    </Button>
                </Box>
            </form>
        </Paper>
    );
}; 