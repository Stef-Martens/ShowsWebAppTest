import axios from 'axios';
import { Show } from '../types/show';

const API_URL = 'https://localhost:7069/api/Shows';

export const getShows = async (): Promise<Show[]> => {
    const response = await axios.get<Show[]>(API_URL);
    return response.data;
}

export const getShow = async (id: number): Promise<Show> => {
    const response = await axios.get<Show>(`${API_URL}/${id}`);
    return response.data;
}

export const addShow = async (show: Show): Promise<Show> => {
    const response = await axios.post<Show>(API_URL, show);
    return response.data;
}

export const updateShow = async (show: Show): Promise<Show> => {
    const response = await axios.put<Show>(`${API_URL}/${show.id}`, show);
    return response.data;
}

export const deleteShow = async (id: number): Promise<void> => {
    await axios.delete(`${API_URL}/${id}`);
}

export const getShowsWithAmountOfSeasons = async (): Promise<Show[]> => {
    const response = await axios.get<Show[]>(`${API_URL}/ShowsFull`);
    response.data.forEach(show => {
        show.seasonsAmount = show.seasons.length;
    });
    return response.data;
}