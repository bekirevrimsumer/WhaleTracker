import React from 'react';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import { ThemeProvider, CssBaseline, Container } from '@mui/material';
import { theme } from './theme';
import Layout from './components/Layout/Layout';
import { Dashboard } from './pages/Dashboard/Dashboard';
import { WalletDetails } from './pages/WalletDetails/WalletDetails';
import { AddWallet } from './pages/AddWallet/AddWallet';

function App() {
  return (
    <ThemeProvider theme={theme}>
      <CssBaseline />
      <BrowserRouter>
        <Layout>
          <Container maxWidth="lg" sx={{ py: 4 }}>
            <Routes>
              <Route path="/" element={<Dashboard />} />
              <Route path="/add-wallet" element={<AddWallet />} />
              <Route path="/wallet/:id" element={<WalletDetails />} />
            </Routes>
          </Container>
        </Layout>
      </BrowserRouter>
    </ThemeProvider>
  );
}

export default App;
