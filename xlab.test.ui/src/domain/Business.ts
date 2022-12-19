export interface Business {
    id: string;
    name: string;
    category: string;
    url: string;
    excerpt: string;
    thumbnail: string;
    lat: number;
    lng: number;
    address: string;
    phone: string;
    twitter: string;
    stars: BusinessStars;
    date: Date;
    tags: string[];
}

export interface BusinessStars {
    beer: number;
    atmosphere: number;
    amenities: number;
    value: number;
}
