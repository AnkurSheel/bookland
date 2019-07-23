import { Link } from 'gatsby';
import React from 'react';
import Layout from '../components/layouts';
import usePosts from '../hooks/use-posts';
import PostPreview from '../components/postPreview';

const IndexPage = () => {
    const posts = usePosts();

    return (
        <Layout>
            <h1>Home</h1>
            <p>What a world.</p>
            <Link to="/about">&rarr; Learn about me</Link>

            <h2>Read my blog</h2>
            {posts.map(post => (
                <PostPreview key={post.slug} post={post}></PostPreview>
            ))}
        </Layout>
    );
};

export default IndexPage;
