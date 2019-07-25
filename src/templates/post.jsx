import React from 'react';
import Layout from '../components/layouts';
import ReadLink from '../components/readLink';
import { MDXRenderer } from 'gatsby-plugin-mdx';
import { graphql } from 'gatsby';

export const query = graphql`
    query PostTemplate($slug: String!) {
        mdx(frontmatter: { slug: { eq: $slug } }) {
            frontmatter {
                title
                author
            }
            body
        }
    }
`;

const PostTemplate = ({ data: { mdx: post } }) => (
    <Layout>
        <h1>{post.frontmatter.title}</h1>
        <p> Posted by {post.frontmatter.author}</p>
        <MDXRenderer>{post.body}</MDXRenderer>
        <ReadLink to="/">&larr; Back to Home</ReadLink>
    </Layout>
);

export default PostTemplate;
