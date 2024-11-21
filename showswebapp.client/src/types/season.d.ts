export interface Season {
    id: number;
    showId: number;
    number: number;
    title: string;
    description: string;
    episodes: Episode[];
    show: Show;
}