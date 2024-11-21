export interface Episode {
    id: number;
    number: number;
    title: string;
    description: string;
    seasonId: number;
    season: Season;
}