import { Grid, Container, Typography, TextField, Paper } from "@mui/material";
import axios, { Axios } from "axios";
import { useEffect, useState } from "react";
import { BusinessBlock } from "./BusinessBlock";
import { Business } from "./domain/Business";

function ConfigureAxios(): Axios {
    return axios.create({
        baseURL: "https://localhost:7048/",
    });
}

export function App() {
    const [searchTerm, setSearchTerm] = useState<string | undefined>(undefined);
    const [businesses, setBusinesses] = useState<Business[]>([]);
    const [httpClient, _] = useState<Axios>(ConfigureAxios);

    useEffect(() => {
        (async () => {
            let search = "";
            if (searchTerm) search = `?tag=${searchTerm}`;

            const results = await httpClient.get<Business[]>(
                `businesses${search}`
            );

            setBusinesses(results.data);
        })();
    }, [searchTerm, setBusinesses]);

    return (
        <Container maxWidth="md" sx={{ margin: "0 auto", textAlign: "center" }}>
            <Container sx={{ padding: 2 }}>
                <Typography variant="h4">XLab Techtest search</Typography>
            </Container>
            <Container sx={{ marginBottom: 1 }}>
                <Paper sx={{ padding: 2 }}>
                    <TextField
                        variant="outlined"
                        label="Search by tag"
                        onChange={(event) =>
                            setSearchTerm(event.currentTarget.value)
                        }
                        sx={{ width: "100%" }}
                    />
                </Paper>
            </Container>
            <Container
                sx={{ overflow: "scroll", maxHeight: "calc(100vh - 200px)" }}
            >
                {businesses.map((business) => (
                    <BusinessBlock data={business} />
                ))}
            </Container>
        </Container>
    );
}
