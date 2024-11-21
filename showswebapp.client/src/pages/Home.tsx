import React, { useState, useEffect } from 'react';
import { getShowsWithAmountOfSeasons } from '../api/shows';
import { Show } from '../types/show';

const Home: React.FC = () => {
    const [shows, setShows] = useState<Show[]>([]);
    const [loading, setLoading] = useState<boolean>(true); // State for loading

    const fetchShows = async () => {
        setLoading(true); // Start loading
        try {
            // Fetch shows from API
            const shows = await getShowsWithAmountOfSeasons();
            console.log(shows);
            setShows(shows);
        }
        catch (error) {
            console.error(error);
        }
        setLoading(false); // Stop loading
    };

    useEffect(() => {
        fetchShows();
    }, []);


    return (
        <div className="overflow-x-auto">
            <h1 className="text-2xl font-semibold mb-4">Shows</h1>
            <button className="btn" onClick={fetchShows}>Refresh</button>
            {loading ? (
                // Show spinner while loading
                <div className="flex justify-center items-center h-20">
                    <div className="spinner border-t-4 border-blue-500 w-8 h-8 rounded-full animate-spin"></div>
                </div>
            ) : (
                <table className="table table-zebra w-full">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Title</th>
                            <th>Description</th>
                            <th>N of Seasons</th>
                        </tr>
                    </thead>
                    <tbody>
                        {shows.length > 0 ? (
                            shows.map(show => (
                                <tr key={show.id}>
                                    <td>{show.id}</td>
                                    <td>{show.title}</td>
                                    <td>{show.description}</td>
                                    <td>{show.seasonsAmount}</td>
                                </tr>
                            ))
                        ) : (
                            <tr>
                                <td colSpan={3} className="text-center">
                                    No shows found / Error fetching shows
                                </td>
                            </tr>
                        )}
                    </tbody>
                </table>
            )}
        </div>
    );
}

export default Home;