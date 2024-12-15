import { createTheme } from '@mui/material';

export const theme = createTheme({
    palette: {
        mode: 'light',
        primary: {
            main: '#1976d2',
        },
        secondary: {
            main: '#dc004e',
        },
    },
    typography: {
        h4: {
            fontWeight: 600,
        },
        h5: {
            fontWeight: 600,
        },
        h6: {
            fontWeight: 600,
        },
    },
    components: {
        MuiCard: {
            styleOverrides: {
                root: {
                    height: '100%',
                    display: 'flex',
                    flexDirection: 'column',
                },
            },
        },
        MuiCardContent: {
            styleOverrides: {
                root: {
                    flexGrow: 1,
                },
            },
        },
    },
}); 