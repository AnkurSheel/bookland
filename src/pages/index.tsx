import { Link } from 'gatsby';
import React from 'react';

const IndexPage = () => (
    <div style={{ color: `purple` }}>
        <h1>Home</h1>
        <p>What a world.</p>
        <Link to="/about">&rarr; Learn about me</Link>
    </div>
);

export default IndexPage;
