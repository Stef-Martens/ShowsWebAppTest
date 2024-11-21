import React from 'react';

const Layout: React.FC<{ children: React.ReactNode }> = ({ children }) => {
    return (
        <div className="min-h-screen flex flex-col">
            {/* Navbar */}
            <header className="bg-blue-500 text-white p-4 w-full">
                <h1 className="text-lg font-bold">Shows Dashboard</h1>
            </header>

            {/* Main Content */}
            <main className="flex-grow p-4">{children}</main>

            {/* Footer */}
            <footer className="bg-gray-800 text-white text-center py-2">
                <p>© 2024 Dashboard enzoowwww</p>
            </footer>
        </div>
    );
};

export default Layout;
