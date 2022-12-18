import { Grid, Paper, Typography } from "@mui/material";
import { Business } from "./domain/Business";

interface BusinessBlockProps {
    data: Business;
}

export function BusinessBlock({ data }: BusinessBlockProps) {
    return (
        <Paper sx={{ padding: 2, marginBottom: 2 }}>
            <Grid container>
                <Grid item sx={{ width: 160 }}>
                    <img src={data.thumbnail} width="100%" />
                </Grid>
                <Grid item xs={9}>
                    <Typography>{data.name}</Typography>
                </Grid>
            </Grid>
        </Paper>
    );
}
