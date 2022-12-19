import {
    Grid,
    Container,
    Typography,
    TextField,
    Paper,
} from "@mui/material";
import axios, { Axios } from "axios";
import { useEffect, useState } from "react";
import { BusinessSection } from "./BusinessSection";
import { Business } from "./domain/Business";

function ConfigureAxios(): Axios {
    return axios.create({
        baseURL: "https://localhost:7048/",
    });
}

function GetTotalPages(header: string | undefined): number {
    return new Number(
        header
            ?.split(",")
            .find((p) => p.includes("rel=last"))
            ?.split("&")[0]
            .slice(20)
    ).valueOf();
}

export function App() {
    const [searchTerm, setSearchTerm] = useState<string | undefined>(undefined);
    const [page, setPage] = useState<number>(1);
    const [totalPages, setTotalPages] = useState<number>(1);
    const [businesses, setBusinesses] = useState<Business[]>([]);
    const [httpClient, _] = useState<Axios>(ConfigureAxios);

    useEffect(() => {
        (async () => {
            let search = "";
            if (searchTerm) search = `&tag=${searchTerm}`;

            const results = await httpClient.get<Business[]>(
                `businesses?pageId=${page}${search}`
            );

            setBusinesses(results.data);
            setTotalPages(GetTotalPages(results.headers["link"]));
        })();
    }, [searchTerm, setBusinesses, page]);

    return (
        <Container maxWidth="md" sx={{ margin: "0 auto", textAlign: "center" }}>
            <Container sx={{ padding: 2 }}>
                <Typography variant="h4" data-cy="app-title">XLab Techtest search</Typography>
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
                        data-cy="app-searchbar"
                    />
                </Paper>
            </Container>
            <Container
                sx={{ overflow: "scroll", maxHeight: "calc(100vh - 200px)" }}
            >
                {businesses.length > 0 ? (
                    <BusinessSection
                        totalPages={totalPages}
                        businesses={businesses}
                        setPageNumber={setPage}
                    />
                ) : (
                    <Container sx={{ padding: 3 }} data-cy="app-nodata">
                        <Typography variant="h6">
                            No businesses found
                        </Typography>
                    </Container>
                )}
            </Container>
        </Container>
    );
}
