import { Chip, Grid, Paper, Typography } from "@mui/material";
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
                <Grid item xs={9} sx={{ paddingLeft: 2, paddingRight: 2 }}>
                    <Typography variant="h6">{data.name}</Typography>
                    <Typography variant="body2">
                        {data.phone
                            ? `${data.phone} - ${data.address}`
                            : data.address}
                    </Typography>
                    <Typography variant="body1">{data.excerpt}</Typography>
                    <Grid
                        container
                        spacing={2}
                        justifyContent="center"
                        sx={{ padding: 2 }}
                    >
                        {data.tags.map((tag, index) => (
                            <Grid item key={index}>
                                <Chip variant="outlined" label={tag} />
                            </Grid>
                        ))}
                    </Grid>
                </Grid>
            </Grid>
        </Paper>
    );
}
