import React from 'react';
import { AppBar, Toolbar, Typography, Box, Container } from '@mui/material';
import { Link } from 'react-router-dom';

interface LayoutProps {
    children: React.ReactNode;
}

const Layout: React.FC<LayoutProps> = ({ children }) => {
    return (
        <>
            <AppBar position="static">
                <Container maxWidth="lg">
                    <Toolbar disableGutters>
                        <Typography
                            variant="h6"
                            component={Link}
                            to="/"
                            sx={{
                                textDecoration: 'none',
                                color: 'inherit',
                                flexGrow: 1
                            }}
                        >
                            WhaleTracker
                        </Typography>
                    </Toolbar>
                </Container>
            </AppBar>
            <Box component="main">
                {children}
            </Box>
        </>
    );
};

export default Layout; 