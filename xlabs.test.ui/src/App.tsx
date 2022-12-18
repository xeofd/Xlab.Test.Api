import {
    Grid,
    Container,
    Typography,
    TextField,
    Paper,
    Button,
} from "@mui/material";
import { useEffect, useState } from "react";
import { Business } from "./domain/Business";

export function App() {
    const [searchTerm, setSearchTerm] = useState<string | undefined>(undefined);
    const [businesses, setBusinesses] = useState<Business[]>([]);

    const handleSearch = () => {
        console.log(searchTerm);
    };

    useEffect(() => {
        (async () => {})();
    }, [searchTerm, setBusinesses]);

    return (
        <Container maxWidth="md" sx={{ margin: "0 auto", textAlign: "center" }}>
            <Container sx={{ padding: 2 }}>
                <Typography variant="h4">XLab Techtest search</Typography>
            </Container>
            <Container>
                <Paper sx={{ padding: 2 }}>
                    <Grid
                        container
                        justifyItems="center"
                        alignItems="center"
                        spacing={2}
                    >
                        <Grid item xs={9}>
                            <TextField
                                variant="outlined"
                                label="Search by tag"
                                onChange={(event) =>
                                    setSearchTerm(event.currentTarget.value)
                                }
                                sx={{ width: "100%" }}
                            />
                        </Grid>
                        <Grid item xs={3}>
                            <Button
                                variant="contained"
                                size="large"
                                sx={{ width: "100%" }}
                                onClick={handleSearch}
                            >
                                Search
                            </Button>
                        </Grid>
                    </Grid>
                </Paper>
            </Container>
            <Grid container></Grid>
        </Container>
    );
}
