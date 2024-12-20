import React from 'react';
import {
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableHead,
    TableRow,
    Paper,
    Avatar,
    Box,
    Button
} from '@mui/material';
import { Token } from '../../types/models';

interface TokenListProps {
    tokens: Token[];
}

const TokenList: React.FC<TokenListProps> = ({ tokens }) => {

    const copyToClipboard = (tokenAddress: string) => {
        navigator.clipboard.writeText(tokenAddress);
    };

    return (
        <TableContainer component={Paper}>
            <Table>
                <TableHead>
                    <TableRow>
                        <TableCell>Token</TableCell>
                        <TableCell>Symbol</TableCell>
                        <TableCell align="right">Balance</TableCell>
                        <TableCell align="right">Price</TableCell>
                        <TableCell align="right">Value</TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {tokens?.map((token) => {
                        const value = token.balance * token.price;
                        
                        return (
                            <TableRow key={token.tokenAddress}>
                                <TableCell>
                                    <Box display="flex" alignItems="center" gap={1}>
                                        {token.tokenIcon && (
                                            <Avatar
                                                src={token.tokenIcon}
                                                sx={{ width: 24, height: 24 }}
                                            />
                                        )}
                                        {token.tokenName}
                                    </Box>
                                </TableCell>
                                <TableCell>{token.tokenSymbol}</TableCell>
                                <TableCell align="right">
                                    {token.balance?.toLocaleString()}
                                </TableCell>
                                <TableCell align="right">
                                    ${token.price?.toFixed(8)}
                                </TableCell>
                                <TableCell align="right">
                                    ${value?.toFixed(8)}
                                </TableCell>
                                <TableCell align="right">
                                    <Button variant="contained" color="primary" onClick={() => copyToClipboard(token.tokenAddress)}>
                                        Copy
                                    </Button>
                                </TableCell>
                            </TableRow>
                        );
                    })}
                </TableBody>
            </Table>
        </TableContainer>
    );
};

export default TokenList; 