export interface Business {
    Id: string;
    Name: string;
    Category: string;
    Url: string;
    Excerpt: string;
    Thumbnail: string;
    Lat: number;
    Lng: number;
    Address: string;
    Phone: string;
    Twitter: string;
    Stars: BusinessStars;
    Date: Date;
    Tags: string[];
}

export interface BusinessStars {
    Beer: number;
    Atmosphere: number;
    Amenities: number;
    Value: number;
}
