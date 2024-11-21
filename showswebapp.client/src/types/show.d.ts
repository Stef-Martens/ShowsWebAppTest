export interface Show {
    id: number;
    title: string;
    genre: string;
    language: string;
    description: string;
    seasons: Season[];
    seasonsAmount: number;
}
