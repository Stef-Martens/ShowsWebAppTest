import React, { useState } from 'react';
import { getShows } from '../api/shows';
import { Show } from '../types/show';


const Home: React.FC = () => {
    // show a button "get shows" that when clicked will put it in a table

    const [shows, setShows] = useState<Show[]>([]);

    const fetchShows = async () => {
        const shows = await getShows();
        setShows(shows);
    }

    return (
        <div className="overflow-x-auto">
            <button onClick={fetchShows}>Get Shows</button>
            <table className="table table-zebra w-full">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Title</th>
                        <th>Description</th>
                    </tr>
                </thead>
                <tbody>
                    {shows.map(show => (
                        <tr key={show.id}>
                            <td>{show.id}</td>
                            <td>{show.title}</td>
                            <td>{show.description}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    )
}

export default Home;