import { Link } from 'gatsby';
import React from 'react';
import Layout from '../components/layouts';

const IndexPage = () => (
    <Layout>
        <h1>Home</h1>
        <p>What a world.</p>
        <Link to="/about">&rarr; Learn about me</Link>
    </Layout>
);

export default IndexPage;
