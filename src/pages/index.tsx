import React from 'react';
import Hero from '../components/hero';
import Layout from '../components/layouts';
import PostPreview from '../components/postPreview';
import usePosts from '../hooks/use-posts';

const IndexPage = () => {
    const posts = usePosts();

    return (
        <>
            <Hero></Hero>
            <Layout>
                <h1>Adventures in Bookland</h1>
                <h2>Read my blog</h2>
                {posts && posts.map(post => <PostPreview key={post.slug} post={post}></PostPreview>)}
            </Layout>
        </>
    );
};

export default IndexPage;
