import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Home from '../pages/Home';
//import EditShow from '../pages/EditShow';


const AppRoutes: React.FC = () => {
    return (
        <Router>
            <Routes>
                <Route path="/" element={<Home />} />
                {/*<Route path="/edit/:id" element={<EditShow />} />*/}
            </Routes>
        </Router>
    );
}

export default AppRoutes;