import { Link } from 'gatsby';
import React from 'react';
const AboutPage = () => (
    <>
        <h1>About Me</h1>
        <Link to="/">&larr; Back to Home</Link>
        <p>This is my personal website</p>
        <img src="https://source.unsplash.com/random/400x200" alt="" />
    </>
);

export default AboutPage;
