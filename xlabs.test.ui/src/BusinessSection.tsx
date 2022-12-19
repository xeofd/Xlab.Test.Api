import { Container, Pagination } from "@mui/material";
import { BusinessBlock } from "./BusinessBlock";
import { Business } from "./domain/Business";

interface BusinessSectionProps {
    totalPages: number;
    setPageNumber: (value: number) => void;
    businesses: Business[];
}

export function BusinessSection({
    totalPages,
    setPageNumber,
    businesses,
}: BusinessSectionProps) {
    return (
        <Container data-cy="app-data">
            <Pagination
                count={totalPages}
                color="secondary"
                variant="outlined"
                onChange={(_, value) => setPageNumber(value)}
                sx={{
                    marginTop: 2,
                    marginBottom: 2,
                }}
                siblingCount={7}
            />
            {businesses.map((business, index) => (
                <BusinessBlock data={business} key={index} />
            ))}
            <Pagination
                count={totalPages}
                color="secondary"
                variant="outlined"
                onChange={(_, value) => setPageNumber(value)}
                sx={{
                    marginTop: 2,
                    marginBottom: 2,
                }}
                siblingCount={7}
            />
            ;
        </Container>
    );
}
