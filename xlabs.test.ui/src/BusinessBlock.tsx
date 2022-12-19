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
                    <img src={data.thumbnail} width="100%" data-cy={`business-thumbnail-${data.id}`} />
                </Grid>
                <Grid item xs={9} sx={{ paddingLeft: 2, paddingRight: 2 }}>
                    <Typography variant="h6" data-cy={`business-name-${data.id}`}>{data.name}</Typography>
                    <Typography variant="body2" data-cy={`business-contact-${data.id}`}>
                        {data.phone
                            ? `${data.phone} - ${data.address}`
                            : data.address}
                    </Typography>
                    <Typography variant="body1" data-cy={`business-excerpt-${data.id}`}>{data.excerpt}</Typography>
                    <Grid
                        container
                        spacing={2}
                        justifyContent="center"
                        sx={{ padding: 2 }}
                    >
                        {data.tags.map((tag, index) => (
                            <Grid item key={index} data-cy={`business-tag-${data.id}-${index}`}>
                                <Chip variant="outlined" label={tag} />
                            </Grid>
                        ))}
                    </Grid>
                </Grid>
            </Grid>
        </Paper>
    );
}
